#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class ItemCommentModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int CommentId { get; set; }

        public CommentModel Comment { get; set; }

        public ItemModel Item { get; set; }
    }
}