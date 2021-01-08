using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class CollectionOptionalField
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int OptionalFieldId { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual OptionalField OptionalField { get; set; }
    }
}
