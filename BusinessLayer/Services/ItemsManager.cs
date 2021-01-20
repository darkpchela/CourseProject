using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class ItemsManager : IItemsManager
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly IModelValidatorsStore validatorsStore;

        private readonly IModelAuthenticatorsStore authenticatorsStore;

        private readonly IResourcesManager resourcesManager;

        private readonly ITagsManager tagsManager;

        private readonly ICollectionsManager collectionsManager;

        private readonly IMapper mapper;

        public ItemsManager(IItemsCrudService itemsCrudService, ICollectionsManager collectionsManager, IModelValidatorsStore validatorsStore, 
            IModelAuthenticatorsStore authenticatorsStore, IResourcesManager resourcesManager, ITagsManager tagsManager, IMapper mapper)
        {
            this.itemsCrudService = itemsCrudService;
            this.mapper = mapper;
            this.collectionsManager = collectionsManager;
            this.validatorsStore = validatorsStore;
            this.authenticatorsStore = authenticatorsStore;
            this.resourcesManager = resourcesManager;
            this.tagsManager = tagsManager;
        }

        public async Task<CreateItemResult> CreateAsync(CreateItemModel createItemModel)
        {
            var authResult = await authenticatorsStore.CreateItemModelAuthenticator.AuthenticateAsync(createItemModel);
            if (!authResult.Succeed)
                return new CreateItemResult(authResult);
            var validResult = await validatorsStore.CreateItemModelValidator.ValidateAsync(createItemModel);
            if (!validResult.Succeed)
                return new CreateItemResult(validResult);
            var itemModel = mapper.Map<ItemModel>(createItemModel);
            await itemsCrudService.CreateAsync(itemModel);
            await collectionsManager.AttachItemToCollection(itemModel.Id, createItemModel.CollectionId);
            await tagsManager.AttachTagsToItemAsync(createItemModel.Tags, itemModel.Id);
            var result = new CreateItemResult
            {
                CreatedItemId = itemModel.Id
            };
            return result;
        }

        public async Task<DeleteItemResult> DeleteAsync(DeleteItemModel deleteItemModel)
        {
            var authResult = await authenticatorsStore.DeleteItemModelAuthenticator.AuthenticateAsync(deleteItemModel);
            if (!authResult.Succeed)
                return new DeleteItemResult(authResult);
            var validResult = await validatorsStore.DeleteItemModelValidator.ValidateAsync(deleteItemModel);
            if (!validResult.Succeed)
                return new DeleteItemResult(validResult);
            var result = new DeleteItemResult();
            var itemResource = (await itemsCrudService.GetAsync(deleteItemModel.ItemId)).Resource;
            await itemsCrudService.DeleteAsync(deleteItemModel.ItemId);
            await resourcesManager.DeleteAsync(itemResource);
            return result;
        }

        public async Task<UpdateItemResult> UpdateAsync(UpdateItemModel updateItemModel)
        {
            var authResult = await authenticatorsStore.UpdateItemModelAuthenticator.AuthenticateAsync(updateItemModel);
            if (!authResult.Succeed)
                return new UpdateItemResult(authResult);
            var validResult = await validatorsStore.UpdateItemModelValidator.ValidateAsync(updateItemModel);
            if (!validResult.Succeed)
                return new UpdateItemResult(validResult);
            var result = new UpdateItemResult();
            var itemModel = mapper.Map<ItemModel>(updateItemModel);
            await tagsManager.AttachTagsToItemAsync(updateItemModel.Tags, itemModel.Id);
            await collectionsManager.AttachItemToCollection(itemModel.Id, updateItemModel.CollectionId);
            await itemsCrudService.UpdateAsync(itemModel);
            return result;
        }
    }
}