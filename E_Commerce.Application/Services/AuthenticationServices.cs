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
        private readonly ITokenServices _tokenServices;

        public AuthenticationServices(IIdentityServices services, ITokenServices tokenServices)
        {
            _services=services;
            _tokenServices=tokenServices;
        }

        public async Task<Result<bool>> CheckEmailAsync(string email, CancellationToken cancellation = default)
        {
            return await _services.EmailExistAsync(email, cancellation);
        }

        public async Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken cancellation = default)
        {
            var userResult = await _services.FindByEmailAsync(email, cancellation);

            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);

            var roles = await _services.GetRoletAsync(userResult.data.Email!, cancellation);
            var tokens = _tokenServices.CreateToken(userResult.data.Id, userResult.data.Email!, userResult.data.UserName!, roles.data);
            return Result<UserDto>.Ok(new UserDto()
            {
                Email = userResult.data.Email!,
                DisplayName = userResult.data.DisplayName!,
                Token = tokens
            });
        }

        public async Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken cancellation = default)
        {
            return await _services.GetUserAddress(email,cancellation);
        }

        public async Task<Result<UserDto>> LoginAsync(LoginDto login, CancellationToken cancellationToken = default)
        {
            var user = await _services.FindByEmailAsync(login.Email,cancellationToken);
            if (!user.IsSuccess)
                return Result<UserDto>.Fail(user.Errors);

            var result = await _services.CheckPasswordAsync(login.Email,login.Password,cancellationToken);
            var roles = await _services.GetRoletAsync(user.data.Email!, cancellationToken);
            var tokens =  _tokenServices.CreateToken(user.data.Id,user.data.Email!,user.data.UserName!,roles.data);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
            return Result<UserDto>.Ok(new UserDto() { 
                Email = user.data.Email!,
                DisplayName= user.data.DisplayName!,
                Token = tokens
            });
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto register, CancellationToken cancellationToken = default)
        {
            var result = await _services.CreateUserAsync(register, cancellationToken);
            if (!result.IsSuccess)
                return Result<UserDto>.Fail(result.Errors);
            var roles = await _services.GetRoletAsync(result.data.Email!, cancellationToken);
            var tokens = _tokenServices.CreateToken(result.data.Id, result.data.Email!, result.data.UserName!, roles.data);
            return Result<UserDto>.Ok(new UserDto()
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                Token =tokens
            });
        }

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string enmail, CancellationToken cancellation = default)
        {
            return await _services.UpdateUserAddressAsync(addressDto, enmail, cancellation);
        }
    }
}
