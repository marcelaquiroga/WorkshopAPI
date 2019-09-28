using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopApp.DTOs
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public virtual CustomerDTO Customer { get; set; }
        public virtual List<SaleDetailDTO> SaleDetail { get; set; }
    }
}
