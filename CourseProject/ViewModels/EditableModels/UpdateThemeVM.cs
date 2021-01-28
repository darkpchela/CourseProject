using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels.EditableModels
{
    public class UpdateThemeVM
    {
        public int ThemeId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 \-]*$")]
        public string Name { get; set; }
    }
}