using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class Comment
    {
        public Comment()
        {
            ItemComments = new HashSet<ItemComment>();
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<ItemComment> ItemComments { get; set; }
    }
}
