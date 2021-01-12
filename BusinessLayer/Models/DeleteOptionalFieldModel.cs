namespace BusinessLayer.Models
{
    public class DeleteOptionalFieldModel
    {
        public bool IsAdminRequest { get; set; }

        public int OwnerId { get; set; }

        public int CollectionId { get; set; }

        public int OptionalFieldId { get; set; }
    }
}