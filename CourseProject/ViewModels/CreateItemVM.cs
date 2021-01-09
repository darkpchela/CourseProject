using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.ViewModels
{
    public class CreateItemVM
    {
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "Name minimum length is 3. Maximum is 64")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Collection must be chosen")]
        public int? CollectionId { get; set; }

        [Required(ErrorMessage = "Item description is required")]
        [StringLength(1024, MinimumLength = 3, ErrorMessage ="Description minimum length is 3. Maximum is 1024")]
        public string Description { get; set; }

        public string Tags { get; set; }

        public IList<EditItemOptionalFieldVM> Fields { get; set; }
    }
}
