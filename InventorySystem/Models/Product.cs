using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventorySystem.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Supplier { get; set; }

        // Relationship: One Product can have many Transactions
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
