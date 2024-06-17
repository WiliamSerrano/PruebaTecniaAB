using System;
using System.Collections.Generic;

namespace PruebaTecniaAB.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SalesProducts = new HashSet<SalesProduct>();
        }

        public int IdSale { get; set; }
        public string NameClient { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PaidDate { get; set; }
        public bool IsPaid { get; set; }

        public virtual ICollection<SalesProduct> SalesProducts { get; set; }
    }
}
