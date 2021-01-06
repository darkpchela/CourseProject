using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<CollectionModel> Collections { get; set; }

        public ICollection<ItemLikeModel> ItemLikes { get; set; }

        public ICollection<ItemModel> Items { get; set; }
    }
}