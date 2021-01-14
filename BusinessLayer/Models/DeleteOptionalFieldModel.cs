using BusinessLayer.Interfaces;

namespace BusinessLayer.Models
{
    public class DeleteOptionalFieldModel : IAuthenticatableModel
    {
        public bool IsAdminRequest { get; set; }

        public int RequesterId { get; set; }

        public int OwnerId { get; set; }

        public int CollectionId { get; set; }

        public int OptionalFieldId { get; set; }
    }
}