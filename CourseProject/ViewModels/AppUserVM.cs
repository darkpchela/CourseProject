namespace CourseProject.ViewModels
{
    public class AppUserVM
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsAdmin { get; set; }

        public string Role { get; set; }
    }
}