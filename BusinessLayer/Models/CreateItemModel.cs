using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models.DALModels;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateItemModel : IAuthenticatableModel
    {
        public string Name { get; set; }

        public int CollectionId { get; set; }

        public int OwnerId { get; set; }

        public int ResourceId { get; set; }

        public string Description { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }

        public IEnumerable<ItemOptionalFieldModel> OptionalFields { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}