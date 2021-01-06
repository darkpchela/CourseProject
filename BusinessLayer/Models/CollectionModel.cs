using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models
{
    public class CollectionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int ThemeId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public UserModel Creator { get; set; }

        public ThemeModel Theme { get; set; }

        public ICollection<CollectionItemModel> CollectionItems { get; set; }

        public ICollection<CollectionOptionalFieldModel> CollectionOptionalFields { get; set; }
    }
}