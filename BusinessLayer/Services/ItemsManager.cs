using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ItemsManager : IItemsManager
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly ICollectionItemCrudService collectionItemCrudService;

        private readonly IResourceCrudService resourceCrudService;

        private readonly IMapper mapper;

        public ItemsManager(IItemsCrudService itemsCrudService, ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService,
            ICollectionItemCrudService collectionItemCrudService, IResourceCrudService resourceCrudService, IMapper mapper)
        {
            this.itemsCrudService = itemsCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.mapper = mapper;
            this.userCrudService = userCrudService;
            this.collectionItemCrudService = collectionItemCrudService;
            this.resourceCrudService = resourceCrudService;
        }

        public async Task<CreateItemResult> CreateAsync(CreateItemModel createItemModel)
        {
            var result = await ValidateCreateModel(createItemModel);
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

        public async Task<UpdateItemResult> UpdateAsync(UpdateItemModel updateItemModel)
        {
            var result = await ValidateUpdateModel(updateItemModel);
            if (!result.Succeed)
                return result;
            var itemModel = mapper.Map<ItemModel>(updateItemModel);
            await itemsCrudService.UpdateAsync(itemModel);
            return result;
        }

        private async Task<CreateItemResult> ValidateCreateModel(CreateItemModel createItemModel)
        {
            var result = new CreateItemResult();
            var collection = await collectionsCrudService.GetAsync(createItemModel.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var owner = await userCrudService.GetAsync(createItemModel.OwnerId);
            if (owner is null)
                result.AddError("User not exists");
            var resource = await resourceCrudService.GetAsync(createItemModel.ResourceId);
            if (resource is null)
                result.AddError("Resource not found");
            if (!result.Succeed)
                return result;

            if (!createItemModel.IsAdminRequest && createItemModel.RequesterId != owner.Id)
                result.AddError("Access denied");
            if (!collection.OptionalFields.Select(of => of.Id).OrderBy(i => i)
                .SequenceEqual(createItemModel.OptionalFields.Select(of => of.OptionalFieldId).OrderBy(i => i)))
                result.AddError("Matching fields failed");
            return result;
        }

        private async Task<UpdateItemResult> ValidateUpdateModel(UpdateItemModel updateItemModel)
        {
            var result = new UpdateItemResult();
            var item = await itemsCrudService.GetAsync(updateItemModel.ItemId);
            if (item is null)
                result.AddError("Item not found");
            var collection = await collectionsCrudService.GetAsync(updateItemModel.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var owner = await userCrudService.GetAsync(updateItemModel.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var resource = await resourceCrudService.GetAsync(updateItemModel.ResourceId);
            if (resource is null)
                result.AddError("Resource not found");
            if (!result.Succeed)
                return result;

            if (!updateItemModel.IsAdminRequest && updateItemModel.RequesterId != owner.Id)
                result.AddError("Access denied");
            if (!collection.OptionalFields.Select(of => of.Id).OrderBy(i => i)
                .SequenceEqual(updateItemModel.OptionalFields.Select(of => of.OptionalFieldId).OrderBy(i => i)))
                result.AddError("Matching fields failed");
            return result;
        }
    }
}