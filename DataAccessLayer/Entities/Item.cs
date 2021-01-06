using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class Item
    {
        public Item()
        {
            CollectionItems = new HashSet<CollectionItem>();
            ItemComments = new HashSet<ItemComment>();
            ItemLikes = new HashSet<ItemLike>();
            ItemOptionalFields = new HashSet<ItemOptionalField>();
            ItemTags = new HashSet<ItemTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<CollectionItem> CollectionItems { get; set; }
        public virtual ICollection<ItemComment> ItemComments { get; set; }
        public virtual ICollection<ItemLike> ItemLikes { get; set; }
        public virtual ICollection<ItemOptionalField> ItemOptionalFields { get; set; }
        public virtual ICollection<ItemTag> ItemTags { get; set; }
    }
}
