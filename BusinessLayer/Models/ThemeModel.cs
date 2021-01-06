using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models
{
    public class ThemeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CollectionModel> Collections { get; set; }
    }
}