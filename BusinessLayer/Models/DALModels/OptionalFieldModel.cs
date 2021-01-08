using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class OptionalFieldModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string Name { get; set; }

        public virtual FieldTypeModel Type { get; set; }

        public IEnumerable<CollectionOptionalFieldModel> CollectionOptionalFields { get; set; }

        public IEnumerable<ItemOptionalFieldModel> ItemOptionalFields { get; set; }
    }
}