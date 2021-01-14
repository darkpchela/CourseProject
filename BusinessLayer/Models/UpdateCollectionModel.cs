using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models.DALModels;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class UpdateCollectionModel : IAuthenticatableModel
    {
        public int CollectionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int OwnerId { get; set; }

        public int ResourceId { get; set; }

        public int ThemeId { get; set; }

        public IEnumerable<OptionalFieldModel> OptionalFields { get; set; }
    }
}