using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public IEnumerable<ItemCommentModel> ItemComments { get; set; }
    }
}