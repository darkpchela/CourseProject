﻿using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollectionsManager
    {
        Task<CreateCollectionResult> CreateAsync(CreateCollectionModel model);

        Task<UpdateCollectionResult> UpdateAsync(UpdateCollectionModel model);

        Task<DeleteCollectionResult> DeleteAsync(DeleteCollectionModel model);

        Task AttachItemToCollection(int itemId, int collectionId);
    }
}