﻿using BusinessLayer.Interfaces.Authentication;

namespace BusinessLayer.Models
{
    public class DeleteOptionalFieldModel : IAuthenticatableModel
    {
        public int OwnerId { get; set; }

        public int CollectionId { get; set; }

        public int OptionalFieldId { get; set; }
    }
}