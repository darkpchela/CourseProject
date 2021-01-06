using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models
{
    public class OptionalFieldModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public ICollection<CollectionOptionalFieldModel> CollectionOptionalFields { get; set; }

        public ICollection<ItemOptionalFieldModel> ItemOptionalFields { get; set; }
    }
}