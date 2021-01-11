using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<CollectionVM> Collections { get; set; }

        public IList<ItemVM> Items { get; set; }
    }
}