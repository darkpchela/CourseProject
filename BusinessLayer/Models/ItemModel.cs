using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int OwnerId { get; set; }

        public DateTime CreationDate { get; set; }

        public UserModel Owner { get; set; }

        public ICollection<CollectionItemModel> CollectionItems { get; set; }

        public ICollection<ItemCommentModel> ItemComments { get; set; }

        public ICollection<ItemLikeModel> ItemLikes { get; set; }

        public ICollection<ItemOptionalFieldModel> ItemOptionalFields { get; set; }

        public ICollection<ItemTagModel> ItemTags { get; set; }
    }
}