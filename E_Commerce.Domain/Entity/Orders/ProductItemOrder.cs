using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entity.Orders
{
    public class ProductItemOrder
    {
        public int ProductId { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string ProductName { get; set; } = default!;
    }
}
