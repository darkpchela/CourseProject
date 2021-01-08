using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class ItemOptionalField
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int OptionalFieldId { get; set; }
        public string Value { get; set; }

        public virtual Item Item { get; set; }
        public virtual OptionalField OptionalField { get; set; }
    }
}
