using BusinessLayer.Interfaces.Authentication;

namespace BusinessLayer.Models
{
    public class DeleteItemModel : IAuthenticatableModel
    {
        public int ItemId { get; set; }

        public int OwnerId { get; set; }
    }
}