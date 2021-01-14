using BusinessLayer.Interfaces;

namespace BusinessLayer.Models
{
    public class CreateDefaultOptionalFieldModel : IAuthenticatableModel
    {
        public bool IsAdminRequest { get; set; }

        public int RequesterId { get; set; }

        public int OwnerId { get; set; }

        public int CollectionId { get; set; }
    }
}