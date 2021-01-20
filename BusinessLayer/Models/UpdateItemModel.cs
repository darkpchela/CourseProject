using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models.DALModels;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class UpdateItemModel : IAuthenticatableModel
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public int CollectionId { get; set; }

        public int OwnerId { get; set; }

        public int ResourceId { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<ItemOptionalFieldModel> OptionalFields { get; set; }
    }
}