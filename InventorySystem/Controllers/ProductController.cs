using Microsoft.AspNetCore.Mvc;
using InventorySystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Show Product List
        public async Task<IActionResult> Index()
        {
            var products = await Task.Run(() => _context.Products.ToList());
            return View(products);
        }

        // ✅ Show Create Product Page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ✅ Handle Product Creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ✅ Show Edit Product Page
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // ✅ Handle Product Edit (AJAX Compatible)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid product data." });
            }

            var existingProduct = await _context.Products.FindAsync(product.ProductID);
            if (existingProduct == null)
            {
                return NotFound(new { success = false, message = "Product not found." });
            }

            existingProduct.Name = product.Name;
            existingProduct.Quantity = product.Quantity;
            existingProduct.Price = product.Price;
            existingProduct.Supplier = product.Supplier;

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Product updated successfully!" });
        }

        // ✅ DELETE Product (AJAX Supported)
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { success = false, message = "Product not found." });
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Product deleted successfully!" });
            }
            catch
            {
                return BadRequest(new { success = false, message = "Failed to delete product." });
            }
        }
    }
}
