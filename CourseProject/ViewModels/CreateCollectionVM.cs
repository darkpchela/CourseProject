using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class CreateCollectionVM
    {
        [Required(ErrorMessage = "Collection name is required.")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Min name legth is 3. Maximum is 64.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Theme must me chosen.")]
        public int? ThemeId { get; set; }

        [Required(ErrorMessage = "Collection description required.")]
        [StringLength(1024, MinimumLength = 3, ErrorMessage = "Min description length is 3. Maximum is 1024.")]
        public string Description { get; set; }

        public IList<CreateOptionalFieldVM> Fields { get; set; }
    }
}