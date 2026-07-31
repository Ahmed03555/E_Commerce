using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IOrderServices
    {
        Task<Result<OrderToResultDto>> CreateOrderAsync(OrderDto orderDto,string email,CancellationToken cancellationToken =default);
        Task<Result<IReadOnlyList<OrderToResultDto>>> GetAllOrdersByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync(CancellationToken ct = default);

        Task<Result<OrderToResultDto>> GetOrderByIdAndEmailAsync(Guid Id, string email, CancellationToken ct = default);


    }
}
