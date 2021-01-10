using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class Resource
    {
        public Resource()
        {
            Collections = new HashSet<Collection>();
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
