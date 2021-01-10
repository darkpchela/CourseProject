using System.Collections.Generic;

namespace BusinessLayer.Models.DALModels
{
    public class ResourceModel
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string PublicId { get; set; }

        public IEnumerable<CollectionModel> Collections { get; set; }

        public IEnumerable<ItemModel> Items { get; set; }
    }
}