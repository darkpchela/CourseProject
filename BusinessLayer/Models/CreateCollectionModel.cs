using BusinessLayer.Models.DALModels;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateCollectionModel
    {
        public string Name { get; set; }

        public int ThemeId { get; set; }

        public string Description { get; set; }

        public IEnumerable<OptionalFieldModel> OptionalFields { get; set; }

        public string ImagePublicKey { get; set; }

        public int OwnerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}