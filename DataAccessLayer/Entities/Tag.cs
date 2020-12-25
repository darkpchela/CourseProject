using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class Tag
    {
        public Tag()
        {
            ItemTags = new HashSet<ItemTag>();
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<ItemTag> ItemTags { get; set; }
    }
}
