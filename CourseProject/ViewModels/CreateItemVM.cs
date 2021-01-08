using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.ViewModels
{
    public class CreateItemVM
    {
        public string Name { get; set; }

        public int CollectionId { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public IList<EditItemOptionalFieldVM> Fields { get; set; }
    }
}
