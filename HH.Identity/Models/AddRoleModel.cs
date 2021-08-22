using System.ComponentModel.DataAnnotations;

namespace HH.Identity.Models
{
    public class AddRoleModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}