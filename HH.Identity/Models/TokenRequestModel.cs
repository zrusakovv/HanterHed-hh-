using System.ComponentModel.DataAnnotations;

namespace HH.Identity.Models
{
    public class TokenRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}