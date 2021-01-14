﻿using BusinessLayer.Models.DALModels;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class UpdateItemModel
    {
        public bool IsAdminRequest { get; set; }

        public int RequesterId { get; set; }

        public int ItemId { get; set; }

        public string Name { get; set; }

        public int CollectionId { get; set; }

        public int OwnerId { get; set; }

        public int ResourceId { get; set; }

        public string Description { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }

        public IEnumerable<ItemOptionalFieldModel> OptionalFields { get; set; }

    }
}