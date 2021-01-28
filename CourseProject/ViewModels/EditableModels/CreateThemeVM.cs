using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels.EditableModels
{
    public class CreateThemeVM
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 \-]*$")]
        public string Name { get; set; }
    }
}