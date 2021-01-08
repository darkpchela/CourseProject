using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<CollectionModel> Collections { get; set; }

        public IEnumerable<ItemLikeModel> ItemLikes { get; set; }

        public IEnumerable<ItemModel> Items { get; set; }
    }
}