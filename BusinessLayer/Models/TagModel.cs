using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models
{
    public class TagModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public ICollection<ItemTagModel> ItemTags { get; set; }
    }
}