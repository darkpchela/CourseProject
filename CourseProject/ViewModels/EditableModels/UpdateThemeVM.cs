using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels.EditableModels
{
    public class UpdateThemeVM
    {
        public int ThemeId { get; set; }

        [Required]
        [RegularExpression(@"^[\w\s]*$")]
        public string Name { get; set; }
    }
}