using HH.Identity.Services;
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
    }
}