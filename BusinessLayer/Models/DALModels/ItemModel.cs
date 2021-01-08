using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int OwnerId { get; set; }

        public DateTime CreationDate { get; set; }

        public UserModel Owner { get; set; }

        public IEnumerable<CollectionItemModel> CollectionItems { get; set; }

        public IEnumerable<ItemCommentModel> ItemComments { get; set; }

        public IEnumerable<ItemLikeModel> ItemLikes { get; set; }

        public IEnumerable<ItemOptionalFieldModel> ItemOptionalFields { get; set; }

        public IEnumerable<ItemTagModel> ItemTags { get; set; }
    }
}