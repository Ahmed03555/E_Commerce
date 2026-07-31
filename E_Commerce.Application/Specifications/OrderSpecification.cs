using E_Commerce.Domain.Entity.Orders;
using E_Commerce.Domain.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class OrderSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderSpecification(string email)
        : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.orderDate);
        }

        public OrderSpecification(Guid id, string email)
            : base(o => o.Id == id && o.BuyerEmail == email)
        {

        }
    }
}
