using System;
using System.Threading.Tasks;
using HH.ApplicationServices.Services.Interfaces;
using HH.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HH.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await userService.RegisterAsync(model);
            return Ok(result);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(TokenRequestModel model)
        {
            var result = await userService.LoginAsync(model);
            
            SetRefreshTokenInCookie(result.RefreshToken);
            
            return Ok(result);
        }
        
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await userService.AddRoleAsync(model);
            return Ok(result);
        }
        
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            
            var response = await userService.RefreshTokenAsync(refreshToken);
            
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            
            return Ok(response);
        }
        
        [Authorize]
        [HttpPost("tokens/{id}")]
        public IActionResult GetRefreshTokens(string id)
        {
            var user = userService.GetById(id);
            return Ok(user.RefreshTokens);
        }
        
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];
            
            if (string.IsNullOrEmpty(token))
                return BadRequest(
                    new
                    {
                        message = "Token is required"
                    });
            
            var response = userService.RevokeToken(token);
            
            if (!response)
                return NotFound(
                    new
                    {
                        message = "Token not found"
                    });
            
            return Ok(
                new
                {
                    message = "Token revoked"
                });
        }
        
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}