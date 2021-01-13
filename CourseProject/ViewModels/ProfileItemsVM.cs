using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class ProfileItemsVM
    {
        public bool AllowEdit { get; set; }

        public IList<ItemVM> Items { get; set; }
    }
}