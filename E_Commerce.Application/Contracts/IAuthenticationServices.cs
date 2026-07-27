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
    }
}
