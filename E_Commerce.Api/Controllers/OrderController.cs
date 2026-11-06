using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ApiBaseController
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices=orderServices;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToResultDto>> CreateOrder([FromBody] OrderDto orderDto, [FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await _orderServices.CreateOrderAsync(orderDto, email, ct));
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethods(CancellationToken ct)
        {
            return ToActionResult(await _orderServices.GetAllDeliveryMethodsAsync(ct));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToResultDto>>> GetAllOrdersByEmail([FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await _orderServices.GetAllOrdersByEmailAsync(email, ct));
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToResultDto>> GetOrderByIdAndEmail(Guid id, [FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await _orderServices.GetOrderByIdAndEmailAsync(id, email, ct));
        }
    }
}
