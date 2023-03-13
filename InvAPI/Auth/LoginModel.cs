using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvAPI.Auth
{
    [Keyless]
    public class LoginModel
    {
       
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
