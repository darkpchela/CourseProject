using AutoMapper;
using BusinessLayer.Etc;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ItemsManager : IItemsManager
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly ICPUnitOfWork cPUnitOfWork;

        private readonly IModelValidatorsStore validatorsStore;

        private readonly IModelAuthenticatorsStore authenticatorsStore;

        private readonly IResourcesManager resourcesManager;

        private readonly ITagsManager tagsManager;

        private readonly ICollectionsManager collectionsManager;

        private readonly IMapper mapper;

        public ItemsManager(IItemsCrudService itemsCrudService, ICollectionsManager collectionsManager, IModelValidatorsStore validatorsStore,
            IModelAuthenticatorsStore authenticatorsStore, IResourcesManager resourcesManager, ITagsManager tagsManager, ICPUnitOfWork cPUnitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.cPUnitOfWork = cPUnitOfWork;
            this.itemsCrudService = itemsCrudService;
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

        public async Task<IEnumerable<ItemModel>> SearchAsync(string text)
        {
            List<Item> entities;
            if (string.IsNullOrEmpty(text))
            {
                entities = await cPUnitOfWork.ItemsRepository.GetAll().OrderByDescending(i => i.CreationDate).Take(25).ToListAsync();
            }
            else
            {
                var conditionsBuilder = new ContainsSearchConditionsBuilder(text);
                var conditions = conditionsBuilder.AllowPrefix(3).AllowNonStrict(2).GetQuery();
                var items = cPUnitOfWork.ItemsRepository.GetAll().Where(i => EF.Functions.Contains(i.Name, conditions) || EF.Functions.Contains(i.Description, conditions) || i.ItemTags.Any(it => text.Contains(it.Tag.Value)));
                var collectionItems = cPUnitOfWork.CollectionsRepository.GetAll().Where(c => EF.Functions.Contains(c.Description, conditions) || EF.Functions.Contains(c.Name, conditions)).SelectMany(c => c.CollectionItems.Select(ci => ci.Item));
                var commentItems = cPUnitOfWork.CommentsRepository.GetAll().Where(c => EF.Functions.Contains(c.Value, conditions)).SelectMany(c => c.ItemComments.Select(ic => ic.Item));
                entities = await items.Concat(collectionItems).Concat(commentItems).Distinct().ToListAsync();
            }
            var resultItems = mapper.Map<IEnumerable<ItemModel>>(entities);
            return resultItems;
        }
    }
}