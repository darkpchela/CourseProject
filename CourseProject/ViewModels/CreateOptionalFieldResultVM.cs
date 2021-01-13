using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class CreateOptionalFieldResultVM
    {
        public bool Succeed { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public int FieldId { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }
    }
}