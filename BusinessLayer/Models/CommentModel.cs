using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<ItemCommentModel> ItemComments { get; set; }
    }
}