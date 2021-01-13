using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class EditItemOptionalFieldVM
    {
        [Required]
        public int? OptionalFieldId { get; set; }

        [Required(ErrorMessage = "Field must not be empty")]
        public string Value { get; set; }

        [Required(ErrorMessage = "Field must have name")]
        public string Name { get; set; }

        public string Type { get; set; }
    }
}