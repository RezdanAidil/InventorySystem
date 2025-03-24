using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class ProductSelectViewModel
    {
        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();

        [Required]
        public int SelectedProductID { get; set; }

        [Required]
        public string Transaction_Type { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        public List<SelectListItem> TransactionTypes { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "IN", Text = "Stock In" },
            new SelectListItem { Value = "OUT", Text = "Stock Out" }
        };
    }
}
