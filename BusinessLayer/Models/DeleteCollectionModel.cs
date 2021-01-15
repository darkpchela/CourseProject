using BusinessLayer.Interfaces.Validators;

namespace BusinessLayer.Models
{
    public class DeleteCollectionModel : IAuthenticatableModel
    {
        public int CollectionId { get; set; }

        public int OwnerId { get; set; }
    }
}