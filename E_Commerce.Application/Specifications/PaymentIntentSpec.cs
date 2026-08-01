using E_Commerce.Domain.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class PaymentIntentSpec : BaseSpecifications<Order, Guid>
    {
        public PaymentIntentSpec(string paymentIntentId) : base(o => o.PaymentIntenId == paymentIntentId)
        {
        }
    }
}
