using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationServices _services;

        public AuthenticationController(IAuthenticationServices services)
        {
            _services=services;
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto,CancellationToken cancellationToken)
        {
            var result =await _services.LoginAsync(loginDto,cancellationToken);
            return ToActionResult(result);
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var result = await _services.RegisterAsync(registerDto, cancellationToken);
            return ToActionResult(result);
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email,CancellationToken cancellationToken = default)
        {
            var result = await _services.CheckEmailAsync(email,cancellationToken);
            return ToActionResult(result);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUaser (CancellationToken cancellationToken = default)
        {
            var result = await _services.GetCurrentUserAsync(GetEmailToken());
            return ToActionResult(result);
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress(CancellationToken cancellationToken = default)
        {
            var result = await _services.GetUserAddress(GetEmailToken(),cancellationToken);
            return ToActionResult(result);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> updateUserAddress(AddressDto addressDto,CancellationToken cancellationToken = default)
        {
            var result = await _services.UpdateUserAddressAsync(addressDto, GetEmailToken(), cancellationToken);
            return ToActionResult(result);
        }

    }
}
