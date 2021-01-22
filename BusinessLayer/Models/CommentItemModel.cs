using System;

namespace BusinessLayer.Models
{
    public class CommentItemModel
    {
        public int ItemId { get; set; }

        public int UserId { get; set; }

        public string Value { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}