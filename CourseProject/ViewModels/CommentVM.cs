using System;

namespace CourseProject.ViewModels
{
    public class CommentVM
    {
        public string Value { get; set; }

        public DateTime CreationDate { get; set; }

        public UserVM User { get; set; }
    }
}