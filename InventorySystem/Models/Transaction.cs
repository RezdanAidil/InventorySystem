using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Required]
        [RegularExpression("^(IN|OUT)$", ErrorMessage = "Transaction Type must be 'IN' or 'OUT'.")]
        public string Transaction_Type { get; set; } // "IN" or "OUT"

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow; // Use UTC for consistency

        // Navigation Property
        public Product Product { get; set; }
        public string ProductName { get; internal set; }
    }
}
