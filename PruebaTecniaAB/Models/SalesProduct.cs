using System;
using System.Collections.Generic;

namespace PruebaTecniaAB.Models
{
    public partial class SalesProduct
    {
        public int IdSalesProduct { get; set; }
        public int IdSale { get; set; }
        public int IdProduct { get; set; }

        public virtual Product oProduct { get; set; } = null!;
        public virtual Sale IdSaleNavigation { get; set; } = null!;
    }
}
