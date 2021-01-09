using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class Collection
    {
        public Collection()
        {
            CollectionItems = new HashSet<CollectionItem>();
            OptionalFields = new HashSet<OptionalField>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ThemeId { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User Creator { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual ICollection<CollectionItem> CollectionItems { get; set; }
        public virtual ICollection<OptionalField> OptionalFields { get; set; }
    }
}
