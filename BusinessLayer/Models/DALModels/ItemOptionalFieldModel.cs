#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class ItemOptionalFieldModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int OptionalFieldId { get; set; }

        public string Value { get; set; }

        public ItemModel Item { get; set; }

        public OptionalFieldModel OptionalField { get; set; }
    }
}