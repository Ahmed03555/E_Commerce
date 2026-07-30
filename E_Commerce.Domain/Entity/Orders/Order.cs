using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entity.Orders
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string buyerEmail, ICollection<OrderItems> items, DeliveryMethod deliveryMethod, OrderAddress shipToAddress, decimal subTotal)
        {
            BuyerEmail=buyerEmail;
            Items=items;
            DeliveryMethod=deliveryMethod;
            ShipToAddress=shipToAddress;
            SubTotal=subTotal;
        }

        public string BuyerEmail { get;private set; } = default!;
        public DateTimeOffset orderDate { get; private set; } = DateTimeOffset.UtcNow;
        public ICollection<OrderItems> Items { get; set; } = [];
        public DeliveryMethod DeliveryMethod { get; private set; } = default!;
        public int DeliveryMethodId { get; private set; }

        public OrderAddress ShipToAddress { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal SubTotal { get; private set; }
        public string PaymentIntenId { get; private set; } = default!;


        public decimal GetTOtal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
        public void MarkPaymentRecive()
        {
            Status = OrderStatus.PaymentRecive;
        }
        public void MarkPaymentFailed()
        {
            Status = OrderStatus.PaymentFailed;
        }
    }
}
