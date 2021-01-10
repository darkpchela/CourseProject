using BusinessLayer.Models.DALModels;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateItemModel
    {
        public string Name { get; set; }

        public int CollectionId { get; set; }

        public int CreatorId { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }

        public IEnumerable<ItemOptionalFieldModel> OptionalFields { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}