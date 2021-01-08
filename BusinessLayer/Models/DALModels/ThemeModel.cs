using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class ThemeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CollectionModel> Collections { get; set; }
    }
}