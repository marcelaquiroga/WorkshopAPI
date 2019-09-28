using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopApp.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual List<SaleDetailDTO> SaleDetail { get; set; }
    }
}
