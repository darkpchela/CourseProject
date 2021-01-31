using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class EditOptionalFieldVM
    {
        [Required]
        public int? FieldId { get; set; }

        [Required(ErrorMessage = "Field type required")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Field name required")]
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Special symbols are forbidden")]
        public string Name { get; set; }
    }
}