using BusinessLayer.Interfaces.Authentication;

namespace BusinessLayer.Models
{
    public class CreateDefaultOptionalFieldModel : IAuthenticatableModel
    {
        public int OwnerId { get; set; }

        public int CollectionId { get; set; }
    }
}