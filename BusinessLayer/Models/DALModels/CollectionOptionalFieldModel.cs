#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class CollectionOptionalFieldModel
    {
        public int Id { get; set; }

        public int CollectionId { get; set; }

        public int OptionalFieldId { get; set; }

        public CollectionModel Collection { get; set; }

        public OptionalFieldModel OptionalField { get; set; }
    }
}