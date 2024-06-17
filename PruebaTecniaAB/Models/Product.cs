using System;
using System.Collections.Generic;

namespace PruebaTecniaAB.Models
{
    public partial class Product
    {
        public Product()
        {
            SalesProducts = new HashSet<SalesProduct>();
        }

        public int IdProduct { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<SalesProduct> SalesProducts { get; set; }
    }
}
