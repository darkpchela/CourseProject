using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IOptionalFieldsCrudService optionalFieldsCrudService;

        private readonly ICollectionOptionalFieldCrudService collectionOptionalFieldCrudService;

        private readonly IThemesCrudService themesCrudService;

        private readonly IMapper mapper;

        public CollectionsManager(ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService,
            ICollectionOptionalFieldCrudService collectionOptionalFieldCrudService, IOptionalFieldsCrudService optionalFieldsCrudService, IThemesCrudService themesCrudService,
            IMapper mapper)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.userCrudService = userCrudService;
            this.collectionOptionalFieldCrudService = collectionOptionalFieldCrudService;
            this.mapper = mapper;
            this.optionalFieldsCrudService = optionalFieldsCrudService;
            this.themesCrudService = themesCrudService;
        }

        public async Task<CreateCollectionResultModel> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var result = new CreateCollectionResultModel();
            if(createCollectionModel is null)
            {
                result.Error = "Model is null";
                return result;
            }
            var collectionModel = mapper.Map<CollectionModel>(createCollectionModel);
            collectionModel.Creator = await userCrudService.GetAsync(createCollectionModel.Owner);
            collectionModel.Theme = await themesCrudService.GetAsync(createCollectionModel.ThemeId);
            if (collectionModel.Creator is null || collectionModel.Theme is null)
            {
                result.Error = "Invalid input parameters";
                return result;
            }
            await collectionsCrudService.CreateAsync(collectionModel);
            if (collectionModel.Id == 0)
            {
                result.Error = "Database error";
                return result;
            }
            foreach (OptionalFieldModel field in createCollectionModel.Fields)
            {
                await optionalFieldsCrudService.CreateAsync(field);
                var collectionField = new CollectionOptionalFieldModel
                {
                    Collection = await collectionsCrudService.GetAsync(collectionModel.Id),
                    OptionalField = await optionalFieldsCrudService.GetAsync(field.Id)
                };
                await collectionOptionalFieldCrudService.CreateAsync(collectionField);
            }
            result.Succeed = true;
            result.CollectionId = collectionModel.Id;
            return result;
        }
    }
}
