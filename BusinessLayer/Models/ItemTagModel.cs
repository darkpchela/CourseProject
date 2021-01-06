#nullable disable

namespace BusinessLayer.Models
{
    public class ItemTagModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int TagId { get; set; }

        public ItemModel Item { get; set; }

        public TagModel Tag { get; set; }
    }
}