using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Domain.Entity.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.Orders
{
    public class OrderToResultDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get;  set; } = default!;
        public DateTimeOffset orderDate { get; set; }
        public ICollection<OrderDto> Items { get; set; } = [];
        public string DeliveryMethod { get;  set; } = default!;
   
        public AddressDto ShipToAddress { get;  set; } = default!;
        public string Status { get;  set; } = default!;

        public decimal SubTotal { get;  set; }
        public decimal Total { get;  set; }

        public decimal DeliveryCost { get; set; }
    }
}
