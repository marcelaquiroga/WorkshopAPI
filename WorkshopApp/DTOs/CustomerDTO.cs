using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopApp.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual List<SaleDTO> Sale { get; set; }
    }
}
