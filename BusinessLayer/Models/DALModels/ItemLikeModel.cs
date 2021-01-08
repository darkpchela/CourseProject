#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class ItemLikeModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int UserId { get; set; }

        public ItemModel Item { get; set; }

        public UserModel User { get; set; }
    }
}