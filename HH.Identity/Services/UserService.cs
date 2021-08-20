using HH.Identity.Models;
using HH.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HH.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT jwt;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwt = jwt.Value;
        }
    }
}