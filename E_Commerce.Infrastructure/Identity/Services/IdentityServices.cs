using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Result<bool>> EmailExistAsync(string email, CancellationToken cancellationToken = default)
        {
            var result = (await _userManager.FindByEmailAsync(email)) is not null;
            return Result<bool>.Ok(result);
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

        public async Task<Result<IReadOnlyList<string>>> GetRoletAsync(string email, CancellationToken cancellationToken = default)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result is null)
            {
                return Result<IReadOnlyList<string>>.Fail(Error.NotFound("User.notFound", "User is Not Found"));
            }
            var roles = await _userManager.GetRolesAsync(result);
            return Result<IReadOnlyList<string>>.Ok(roles.ToList());
        }

        public async Task<Result<AddressDto>> GetUserAddress(string email, CancellationToken cancellation = default)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email, cancellation);

            if (user is null)
                return Result<AddressDto>.Fail(Error.NotFound("User.NotFound", "User Not Found"));

            if (user.Address is null)
                return Result<AddressDto>.Fail(Error.NotFound("UserAddress.NotFound", "Address Not Found"));

            return Result<AddressDto>.Ok(new AddressDto()
            {
                FirstName = user.Address.FirstName,
                LastName = user.Address.LastName,
                City = user.Address.City,
                Street = user.Address.Street,
                Country = user.Address.Country,
            });
        }

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto, string enmail, CancellationToken cancellation = default)
        {
            var user = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == enmail, cancellation);

            if (user is null)
                return Result<AddressDto>.Fail(Error.NotFound("User.NotFound", "User Not Found"));

            if (user.Address is null)
            {
                user.Address = new Address()
                {
                    FirstName = addressDto.FirstName,
                    LastName = addressDto.LastName,
                    Street = addressDto.Street,
                    City = addressDto.City,
                    Country = addressDto.Country
                };
            }
            else
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Street = addressDto.Street;
                user.Address.Country = addressDto.Country;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return Result<AddressDto>.Fail(Error.Failure("UserAddress.Failure", string.Join("; ", result.Errors.Select(E => E.Description))));

            return Result<AddressDto>.Ok(addressDto);
        }
    }
}
