namespace BusinessLayer.Models
{
    public class UserSignInModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}