using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecniaAB.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SalesProducts = new HashSet<SalesProduct>();
        }

        public int IdSale { get; set; }

        [Required]
        public string NameClient { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Mail { get; set; } = null!;

        [Required]
        public decimal TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PaidDate { get; set; }
        public bool IsPaid { get; set; }

        [Required]
        public virtual ICollection<SalesProduct> SalesProducts { get; set; }
    }
}
