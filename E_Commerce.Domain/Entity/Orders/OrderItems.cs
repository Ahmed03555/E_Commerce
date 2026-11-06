using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entity.Orders
{
    public class OrderItems : BaseEntity<int>
    {
        public ProductItemOrder productItemOrder { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
