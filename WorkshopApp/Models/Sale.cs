using System;
using System.Collections.Generic;

namespace WorkshopApp.Models
{
    public partial class Sale
    {
        public Sale()
        {
            SaleDetail = new HashSet<SaleDetail>();
        }

        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleDetail> SaleDetail { get; set; }
    }
}
