using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace HrManegment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }


        [HttpGet("LogoutAllUsers")]
        public ActionResult LogoutAllUsers()
        {
            _authenticationService.LogoutAllUsers();
            return Ok();
        }


        [HttpGet("ClearCach")]
        public ActionResult ClearCach()
        {
            _authenticationService.ClearCach();
            return Ok();
        }


        [HttpPost("AddToCach")]
        public ActionResult AddToCach(List<string> UsersId)
        {
            _authenticationService.AddToCash(UsersId);
            return Ok();
        }

        [HttpPost("refresh-token")]

        
        public async Task<IActionResult> RefreshToken([FromBody] string userRefreshToken)
        {
            try
            {
                var result = await _authenticationService.RefreshToken(userRefreshToken);
                return Ok( new { Token= result.Item1, RefreshToken=result.Item2 });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}