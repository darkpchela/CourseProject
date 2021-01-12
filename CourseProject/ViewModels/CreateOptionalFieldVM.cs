using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class CreateOptionalFieldVM
    {
        //public int Id { get; set; }

        [Required]
        public int? CollectionId { get; set; }

        [Required(ErrorMessage = "Choose optional field type.")]
        public int? TypeId { get; set; }

        [Required(ErrorMessage = "Optional field name is required")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Name minimum length is 3. Maximum is 64.")]
        public string Name { get; set; }
    }
}