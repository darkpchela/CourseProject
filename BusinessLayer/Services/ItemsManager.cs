using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ItemsManager : IItemsManager
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly ICollectionItemCrudService collectionItemCrudService;

        private readonly IModelValidatorsStore validatorsStore;

        private readonly IModelAuthenticatorsStore authenticatorsStore;

        private readonly IMapper mapper;

        public ItemsManager(IItemsCrudService itemsCrudService, ICollectionItemCrudService collectionItemCrudService, IModelValidatorsStore validatorsStore, IModelAuthenticatorsStore authenticatorsStore, IMapper mapper)
        {
            this.itemsCrudService = itemsCrudService;
            this.mapper = mapper;
            this.collectionItemCrudService = collectionItemCrudService;
            this.validatorsStore = validatorsStore;
            this.authenticatorsStore = authenticatorsStore;
        }

        public async Task<CreateItemResult> CreateAsync(CreateItemModel createItemModel)
        {
            var validResult = await validatorsStore.CreateItemModelValidator.ValidateAsync(createItemModel);
            if (!validResult.Succeed)
                return new CreateItemResult(validResult);
            var authResult = await authenticatorsStore.CreateItemModelAuthenticator.AuthenticateAsync(createItemModel);
            if (!authResult.Succeed)
                return new CreateItemResult(authResult);
            var result = new CreateItemResult();
            var itemModel = mapper.Map<ItemModel>(createItemModel);
            await itemsCrudService.CreateAsync(itemModel);
            await AttachItemToCollection(itemModel.Id, createItemModel.CollectionId);
            result.ItemId = itemModel.Id;
            return result;
        }

        public async Task<DeleteItemResult> DeleteAsync(DeleteItemModel deleteItemModel)
        {
            var validResult = await validatorsStore.DeleteItemModelValidator.ValidateAsync(deleteItemModel);
            if (!validResult.Succeed)
                return new DeleteItemResult(validResult);
            var authResult = await authenticatorsStore.DeleteItemModelAuthenticator.AuthenticateAsync(deleteItemModel);
            if (!authResult.Succeed)
                return new DeleteItemResult(authResult);
            var result = new DeleteItemResult();
            await itemsCrudService.DeleteAsync(deleteItemModel.ItemId);
            return result;
        }

        public async Task<UpdateItemResult> UpdateAsync(UpdateItemModel updateItemModel)
        {
            var validResult = await validatorsStore.UpdateItemModelValidator.ValidateAsync(updateItemModel);
            if (!validResult.Succeed)
                return new UpdateItemResult(validResult);
            var authResult = await authenticatorsStore.UpdateItemModelAuthenticator.AuthenticateAsync(updateItemModel);
            if (!authResult.Succeed)
                return new UpdateItemResult(authResult);
            var result = new UpdateItemResult();
            var itemModel = mapper.Map<ItemModel>(updateItemModel);
            await itemsCrudService.UpdateAsync(itemModel);
            return result;
        }

        private async Task AttachItemToCollection(int itemId, int collectionId)
        {
            var collectionItem = new CollectionItemModel
            {
                CollectionId = collectionId,
                ItemId = itemId
            };
            await collectionItemCrudService.CreateAsync(collectionItem);
        }
    }
}