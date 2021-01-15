using BusinessLayer.Interfaces.Authenticators;

namespace BusinessLayer.Models
{
    public class DeleteCollectionModel : IAuthenticatableModel
    {
        public int CollectionId { get; set; }

        public int OwnerId { get; set; }
    }
}