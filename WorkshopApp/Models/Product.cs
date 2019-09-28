using System;
using System.Collections.Generic;

namespace WorkshopApp.Models
{
    public partial class Product
    {
        public Product()
        {
            SaleDetail = new HashSet<SaleDetail>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<SaleDetail> SaleDetail { get; set; }
    }
}
