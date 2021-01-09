using BusinessLayer.Models.DALModels;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateItemModel
    {
        public string Name { get; set; }

        public int CollectionId { get; set; }

        public string ImageId { get; set; }

        public string Description { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }

        public IEnumerable<ItemOptionalFieldModel> Fields { get; set; }
    }
}