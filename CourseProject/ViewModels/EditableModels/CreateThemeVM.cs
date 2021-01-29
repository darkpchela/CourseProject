using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels.EditableModels
{
    public class CreateThemeVM
    {
        [Required]
        [RegularExpression(@"^[\w\s]*$")]
        public string Name { get; set; }
    }
}