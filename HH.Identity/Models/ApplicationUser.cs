using System.Collections.Generic;
using HH.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace HH.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}