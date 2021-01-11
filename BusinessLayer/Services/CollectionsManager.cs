﻿using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IThemesCrudService themesCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IMapper mapper;

        public CollectionsManager(ICollectionsCrudService collectionsCrudService, IThemesCrudService themesCrudService,
            IUserCrudService userCrudService, IMapper mapper)
        {
            this.mapper = mapper;
            this.userCrudService = userCrudService;
            this.themesCrudService = themesCrudService;
            this.collectionsCrudService = collectionsCrudService;
        }

        public async Task<CreateCollectionResultModel> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var result = await ValidateCreateCollectionModel(createCollectionModel);
            if (!result.Succeed)
                return result;
            var collectionModel = mapper.Map<CollectionModel>(createCollectionModel);
            await collectionsCrudService.CreateAsync(collectionModel);
            result.CollectionId = collectionModel.Id;
            return result;
        }

        public async Task<UpdateCollectionResultModel> UpdateAsync(UpdateCollectionModel updateCollectionModel)
        {
            var result = await ValidateUpdateCollectionModel(updateCollectionModel);
            if (!result.Succeed)
                return result;
            return result;
        }

        private async Task<CreateCollectionResultModel> ValidateCreateCollectionModel(CreateCollectionModel createCollectionModel)
        {
            var result = new CreateCollectionResultModel();
            var theme = await themesCrudService.GetAsync(createCollectionModel.ThemeId);
            if (theme is null)
                result.Errors.Add("Theme not exists");
            var user = await userCrudService.GetAsync(createCollectionModel.OwnerId);
            if (user is null)
                result.Errors.Add("User not exists");
            return result;
        }

        private async Task<UpdateCollectionResultModel> ValidateUpdateCollectionModel(UpdateCollectionModel updateCollectionModel)
        {
            var result = new UpdateCollectionResultModel();
            return result;
        }
    }
}