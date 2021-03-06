﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLayer.Models.DALModels
{
    public class CollectionModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OwnerId { get; set; }

        public int ResourceId { get; set; }

        public int ThemeId { get; set; }

        public DateTime CreationDate { get; set; }

        public UserModel Owner { get; set; }

        public ResourceModel Resource { get; set; }

        public ThemeModel Theme { get; set; }

        public IEnumerable<CollectionItemModel> CollectionItems { get; set; }

        public IEnumerable<OptionalFieldModel> OptionalFields { get; set; }
    }
}