using System;
using System.Collections.Generic;

namespace WorkshopApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sale = new HashSet<Sale>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Sale> Sale { get; set; }
    }
}
