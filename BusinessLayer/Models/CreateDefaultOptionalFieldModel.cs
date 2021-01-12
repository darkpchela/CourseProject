namespace BusinessLayer.Models
{
    public class CreateDefaultOptionalFieldModel
    {
        public bool IsAdminRequest { get; set; }

        public int OwnerId { get; set; }

        public int CollectionId { get; set; }
    }
}