using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Services
{
    internal class IdentityServices : IIdentityServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityServices(UserManager<ApplicationUser> userManager)
        {
            _userManager=userManager;
        }
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            var Usere = await _userManager.FindByEmailAsync(email);
            if (Usere is null)
            {
                return Result<bool>.Fail(Error.Unauthorized("User.Unauthorized", "User is Unauthorized"));
            }
            var result = await _userManager.CheckPasswordAsync(Usere, password);
          
                return result ? Result<bool>.Ok(true): Result<bool>.Fail(Error.Unauthorized("User.Unauthorized", "User is Unauthorized"));
            
        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken cancellationToken = default)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.Phone,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.Select(E => new Error(E.Code, E.Description)).ToList();

                return Result<IdentityUserResult>.Fail(error);
            }

            return Result<IdentityUserResult>.Ok(new IdentityUserResult(user.Id, user.Email, user.DisplayName, user.UserName));

        }

        public async Task<Result<IdentityUserResult>> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if(result is null)
            {
                return Result<IdentityUserResult>.Fail(Error.NotFound("User.notFound", "User is Not Found"));
            }
            return Result<IdentityUserResult>.Ok(new IdentityUserResult(result.Id, result.Email, result.DisplayName, result.UserName));
        }
    }
}
