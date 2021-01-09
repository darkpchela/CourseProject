using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ItemsManager : IItemsManager
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IItemOptionalFieldCrudService itemOptionalFieldCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IMapper mapper;

        public ItemsManager(IItemsCrudService itemsCrudService, ICollectionsCrudService collectionsCrudService, IItemOptionalFieldCrudService itemOptionalFieldCrudService, IUserCrudService userCrudService, IMapper mapper)
        {
            this.itemsCrudService = itemsCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.itemOptionalFieldCrudService = itemOptionalFieldCrudService;
            this.mapper = mapper;
            this.userCrudService = userCrudService;
        }

        public async Task CreateAsync(CreateItemModel createItemModel)
        {
            var itemModel = mapper.Map<ItemModel>(createItemModel);
            await itemsCrudService.CreateAsync(itemModel);
        }
    }
}