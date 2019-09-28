using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopApp.DTOs
{
    public class SaleDetailDTO
    {
        public int SaleDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public virtual ProductDTO Product { get; set; }
        public virtual SaleDTO SaleDetailNavigation { get; set; }
    }
}
