using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class CollectionVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ThemeVM Theme { get; set; }

        public UserVM Owner { get; set; }

        public string Description { get; set; }

        public IList<ItemVM> Items { get; set; }
    }
}