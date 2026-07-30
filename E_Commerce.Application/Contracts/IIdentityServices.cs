using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IIdentityServices
    {
        Task<Result<IdentityUserResult>> FindByEmailAsync(string email,CancellationToken cancellationToken = default);
        Task<Result<bool>> CheckPasswordAsync(string email, string password,CancellationToken cancellationToken = default);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto,CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<string>>> GetRoletAsync(string email,CancellationToken cancellationToken = default);
        Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken cancellation = default);
        Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string enmail, CancellationToken cancellation = default);

        Task<Result<bool>> EmailExistAsync(string email, CancellationToken cancellationToken = default)   
    }
}
