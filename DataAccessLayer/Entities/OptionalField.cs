using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class OptionalField
    {
        public OptionalField()
        {
            CollectionOptionalFields = new HashSet<CollectionOptionalField>();
            ItemOptionalFields = new HashSet<ItemOptionalField>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<CollectionOptionalField> CollectionOptionalFields { get; set; }
        public virtual ICollection<ItemOptionalField> ItemOptionalFields { get; set; }
    }
}
