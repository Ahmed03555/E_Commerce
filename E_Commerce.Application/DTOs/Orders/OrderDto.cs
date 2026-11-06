using E_Commerce.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.Orders
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; } = default!;
        [Required]
        public int DeliveryMethodId { get; set; } = default!;

        [Required]
        public AddressDto ShipToAddress { get; set; }= default!;
    }
}
