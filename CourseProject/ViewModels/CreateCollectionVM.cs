using BusinessLayer.Models;
using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class CreateCollectionVM
    {
        public string Name { get; set; }

        public ThemeModel Theme { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IList<OptionalFieldModel> Fields { get; set; }
    }
}