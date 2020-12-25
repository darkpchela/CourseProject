﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Entities
{
    public partial class ItemTag
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int TagId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
