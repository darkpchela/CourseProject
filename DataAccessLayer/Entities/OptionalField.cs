#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class OptionalField
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual FieldType Type { get; set; }
    }
}