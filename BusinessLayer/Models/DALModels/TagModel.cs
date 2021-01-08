using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class TagModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public IEnumerable<ItemTagModel> ItemTags { get; set; }
    }
}