using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class Theme
    {
        public Theme()
        {
            Collections = new HashSet<Collection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
    }
}
