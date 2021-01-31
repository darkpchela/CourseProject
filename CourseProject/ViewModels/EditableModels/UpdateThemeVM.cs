using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels.EditableModels
{
    public class UpdateThemeVM
    {
        public int ThemeId { get; set; }

        [Required(ErrorMessage ="Theme name is required")]
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Special symbols are forbidden")]
        public string Name { get; set; }
    }
}