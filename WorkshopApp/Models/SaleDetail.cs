using System;
using System.Collections.Generic;

namespace WorkshopApp.Models
{
    public partial class SaleDetail
    {
        public int SaleDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public virtual Product Product { get; set; }
        public virtual Sale SaleDetailNavigation { get; set; }
    }
}
