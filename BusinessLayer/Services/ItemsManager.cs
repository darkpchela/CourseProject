using AutoMapper;
using BusinessLayer.Interfaces;
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

        private readonly IModelValidatorsStore validationUnitOfWork;

        private readonly IMapper mapper;

        public ItemsManager(IItemsCrudService itemsCrudService, ICollectionItemCrudService collectionItemCrudService, IModelValidatorsStore validationUnitOfWork, IMapper mapper)
        {
            this.itemsCrudService = itemsCrudService;
            this.mapper = mapper;
            this.collectionItemCrudService = collectionItemCrudService;
            this.validationUnitOfWork = validationUnitOfWork;
        }

        public async Task<CreateItemResult> CreateAsync(CreateItemModel createItemModel)
        {
            var validationResult = await validationUnitOfWork.CreateItemModelValidator.ValidateAsync(createItemModel);
            var result = new CreateItemResult(validationResult);
            if (!result.Succeed)
                return result;
            var itemModel = mapper.Map<ItemModel>(createItemModel);
            await itemsCrudService.CreateAsync(itemModel);
            var collectionItem = new CollectionItemModel
            {
                CollectionId = createItemModel.CollectionId,
                ItemId = itemModel.Id
            };
            await collectionItemCrudService.CreateAsync(collectionItem);
            result.ItemId = itemModel.Id;
            return result;
        }

        public Task<DeleteItemResult> DeleteAsync(DeleteItemModel deleteItemModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UpdateItemResult> UpdateAsync(UpdateItemModel updateItemModel)
        {
            var validationResult = await validationUnitOfWork.UpdateItemModelValidator.ValidateAsync(updateItemModel);
            var result = new UpdateItemResult(validationResult);
            if (!result.Succeed)
                return result;
            var itemModel = mapper.Map<ItemModel>(updateItemModel);
            await itemsCrudService.UpdateAsync(itemModel);
            return result;
        }
    }
}