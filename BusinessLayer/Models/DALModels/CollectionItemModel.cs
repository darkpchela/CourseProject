#nullable disable

namespace BusinessLayer.Models
{
    public class CollectionItemModel
    {
        public int Id { get; set; }

        public int CollectionId { get; set; }

        public int ItemId { get; set; }

        public CollectionModel Collection { get; set; }

        public ItemModel Item { get; set; }
    }
}