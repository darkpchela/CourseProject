using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class ItemComment
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Item Item { get; set; }
    }
}
