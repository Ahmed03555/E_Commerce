using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface  IAuthenticationServices
    {
        Task<Result<UserDto>> LoginAsync(LoginDto login, CancellationToken cancellationToken = default);
        Task<Result<UserDto>> RegisterAsync(RegisterDto register, CancellationToken cancellationToken = default);

        Task<Result<bool>> CheckEmailAsync(string email, CancellationToken cancellation = default);
        Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken cancellation = default);

        Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken cancellation = default);
        Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string enmail,CancellationToken cancellation = default);
    }
}
