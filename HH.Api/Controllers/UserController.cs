using System.Threading.Tasks;
using HH.ApplicationServices.Services.Interfaces;
using HH.Identity.Models;
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
        
        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await userService.GetTokenAsync(model);
            return Ok(result);
        }
        
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await userService.AddRoleAsync(model);
            return Ok(result);
        }
    }
}