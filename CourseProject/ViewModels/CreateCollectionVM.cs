using BusinessLayer.Models;
using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class CreateCollectionVM
    {
        public string Name { get; set; }

        public int ThemeId { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IList<CollectionOptionalFieldVM> Fields { get; set; }

        public IEnumerable<OptionalFieldModel> OptionalFields{ get; set; }

        public IEnumerable<ThemeModel> Themes { get; set; }

    }
}