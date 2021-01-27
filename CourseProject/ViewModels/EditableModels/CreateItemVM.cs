using CourseProject.ViewModels.EditableModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class CreateItemVM
    {
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Name minimum length is 3. Maximum is 64")]
        [RegularExpression(@"^[a-zA-Z0-9 \-]*$")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Collection must be chosen")]
        public int? CollectionId { get; set; }

        [Required(ErrorMessage = "Image required")]
        public int? ResourceId { get; set; }

        [Required]
        public int? OwnerId { get; set; }

        [Required(ErrorMessage = "Item description is required")]
        [StringLength(1024, MinimumLength = 3, ErrorMessage = "Description minimum length is 3. Maximum is 1024")]
        public string Description { get; set; }

        public string TagsJson { get; set; }

        public IList<CreateItemOptionalFieldVM> Fields { get; set; }

        public string ImageUrl { get; set; }
    }
}