using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}