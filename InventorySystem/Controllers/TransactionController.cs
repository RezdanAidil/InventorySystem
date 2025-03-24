using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InventorySystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ApplicationDbContext context, ILogger<TransactionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ✅ Show Transactions List
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Product)
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            return View(transactions);
        }

        // ✅ Show Create Transaction Form
        public IActionResult Create()
        {
            var products = _context.Products
                .Select(p => new SelectListItem
                {
                    Value = p.ProductID.ToString(),
                    Text = $"{p.Name} (Stock: {p.Quantity})"
                })
                .ToList();

            var viewModel = new ProductSelectViewModel
            {
                Products = products,
                TransactionTypes = new List<SelectListItem>
                {
                    new SelectListItem { Value = "IN", Text = "Stock In" },
                    new SelectListItem { Value = "OUT", Text = "Stock Out" }
                }
            };

            return View(viewModel);
        }

        // ✅ Handle Create Transaction (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] ProductSelectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid input. Please check your entries." });
            }

            var product = await _context.Products.FindAsync(model.SelectedProductID);
            if (product == null)
            {
                _logger.LogWarning("Attempted transaction with a non-existent product ID: {ProductID}", model.SelectedProductID);
                return Json(new { success = false, message = "Product not found." });
            }

            if (model.Transaction_Type == "OUT" && model.Quantity > product.Quantity)
            {
                _logger.LogWarning("Insufficient stock: Tried to remove {Quantity} but only {Stock} available.", model.Quantity, product.Quantity);
                return Json(new { success = false, message = "Not enough stock available!" });
            }

            var transaction = new Transaction
            {
                ProductID = model.SelectedProductID,
                ProductName = product.Name,
                Transaction_Type = model.Transaction_Type,
                Quantity = model.Quantity,
                Date = DateTime.Now
            };

            // ✅ Update Stock
            if (transaction.Transaction_Type == "IN")
            {
                product.Quantity += transaction.Quantity;
            }
            else if (transaction.Transaction_Type == "OUT")
            {
                product.Quantity -= transaction.Quantity;
            }

            try
            {
                _context.Products.Update(product);
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Transaction successful: {TransactionType} {Quantity} units of {ProductName}",
                                        transaction.Transaction_Type, transaction.Quantity, transaction.ProductName);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing transaction.");
                return Json(new { success = false, message = "An error occurred while processing the transaction." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound(new { success = false, message = "Transaction not found." });
            }

            try
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Transaction deleted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Failed to delete transaction.", error = ex.Message });
            }
        }

    }
}
