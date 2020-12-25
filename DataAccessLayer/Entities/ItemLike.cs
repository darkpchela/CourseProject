using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class ItemLike
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }

        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}
