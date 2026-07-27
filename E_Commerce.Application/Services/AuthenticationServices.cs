using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class AuthenticationServices : IAuthenticationServices
    {
        private readonly IIdentityServices _services;

        public AuthenticationServices(IIdentityServices services)
        {
            _services=services;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto login, CancellationToken cancellationToken = default)
        {
            var user = await _services.FindByEmailAsync(login.Email,cancellationToken);
            if (!user.IsSuccess)
                return Result<UserDto>.Fail(user.Errors);

            var result = await _services.CheckPasswordAsync(login.Email,login.Password,cancellationToken);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
            return Result<UserDto>.Ok(new UserDto() { 
                Email = user.data.Email!,
                DisplayName= user.data.DisplayName!,
                Token = "Token - To DO"
                });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto register, CancellationToken cancellationToken = default)
        {
            var result = await _services.CreateUserAsync(register, cancellationToken);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
            return Result<UserDto>.Ok(new UserDto()
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                Token ="Token - To DO"
            });
        }
    }
}
