using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class CollectionItem
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int ItemId { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual Item Item { get; set; }
    }
}
