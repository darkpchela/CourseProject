﻿using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ItemsManager : IItemsManager
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly ICollectionItemCrudService collectionItemCrudService;

        private readonly IMapper mapper;

        public ItemsManager(IItemsCrudService itemsCrudService, ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService,
            ICollectionItemCrudService collectionItemCrudService, IMapper mapper)
        {
            this.itemsCrudService = itemsCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.mapper = mapper;
            this.userCrudService = userCrudService;
            this.collectionItemCrudService = collectionItemCrudService;
        }

        public async Task<CreateItemResultModel> CreateAsync(CreateItemModel createItemModel)
        {
            var result = await ValidateModel(createItemModel);
            if (result.Errors.Count > 0)
                return result;
            var itemModel = mapper.Map<ItemModel>(createItemModel);
            await itemsCrudService.CreateAsync(itemModel);
            var collectionItem = new CollectionItemModel
            {
                CollectionId = createItemModel.CollectionId,
                ItemId = itemModel.Id
            };
            await collectionItemCrudService.CreateAsync(collectionItem);
            result.Succeed = true;
            return result;
        }

        private async Task<CreateItemResultModel> ValidateModel(CreateItemModel createItemModel)
        {
            var result = new CreateItemResultModel();
            var collection = await collectionsCrudService.GetAsync(createItemModel.CollectionId);
            if (collection is null)
                result.Errors.Add("Collection not exists");
            var user = await userCrudService.GetAsync(createItemModel.CreatorId);
            if (user is null)
                result.Errors.Add("User not exists");
            return result;
        }
    }
}