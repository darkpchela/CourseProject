using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class User
    {
        public User()
        {
            Collections = new HashSet<Collection>();
            Comments = new HashSet<Comment>();
            ItemLikes = new HashSet<ItemLike>();
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ItemLike> ItemLikes { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
