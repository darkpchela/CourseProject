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
        [RegularExpression(@"^[a-zA-Z0-9 \-]*$")]
        public string Name { get; set; }
    }
}