using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class EditItemOptionalFieldVM
    {
        public int ItemFieldId { get; set; }

        [Required]
        public int? FieldId { get; set; }

        public string Value { get; set; }

        [Required(ErrorMessage = "Field must have name")]
        [RegularExpression(@"^[\w\s]*$")]
        public string Name { get; set; }

        public string Type { get; set; }
    }
}