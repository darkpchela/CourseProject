using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Validators;

namespace BusinessLayer.Models
{
    public class CreateDefaultOptionalFieldModel : IAuthenticatableModel
    {
        public int OwnerId { get; set; }

        public int CollectionId { get; set; }
    }
}