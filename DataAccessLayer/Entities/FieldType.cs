using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class FieldType
    {
        public FieldType()
        {
            OptionalFields = new HashSet<OptionalField>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<OptionalField> OptionalFields { get; set; }
    }
}
