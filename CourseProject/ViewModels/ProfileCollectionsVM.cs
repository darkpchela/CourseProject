using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class ProfileCollectionsVM
    {
        public IList<CollectionVM> Collections { get; set; }

        public bool IsEditable { get; set; }
    }
}