﻿using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class OptionalFieldsManager : IOptionalFieldsManager
    {
        private readonly IOptionalFieldsCrudService optionalFieldsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IFieldTypesCrudService fieldTypesCrudService;

        public OptionalFieldsManager(IOptionalFieldsCrudService optionalFieldsCrudService, ICollectionsCrudService collectionsCrudService, IFieldTypesCrudService fieldTypesCrudService)
        {
            this.optionalFieldsCrudService = optionalFieldsCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.fieldTypesCrudService = fieldTypesCrudService;
        }

        public async Task<CreateOptionalFieldResultModel> CreateDefaultAsync(int collectionId)
        {
            var result = await ValidateCreateDefaultRequest(collectionId);
            if (!result.Succeed)
                return result;
            var model = new OptionalFieldModel
            {
                CollectionId = collectionId,
                Name = "Unnamed",
                TypeId = (await fieldTypesCrudService.GetAllAsync()).First().Id
            };
            await optionalFieldsCrudService.CreateAsync(model);
            result.OptionalFieldModel = model;
            return result;
        }

        private async Task<CreateOptionalFieldResultModel> ValidateCreateDefaultRequest(int collectionId)
        {
            var result = new CreateOptionalFieldResultModel();
            var collection = await collectionsCrudService.GetAsync(collectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var type = (await fieldTypesCrudService.GetAllAsync()).FirstOrDefault();
            if (type is null)
                result.AddError("Available field types not found ");
            return result;
        }
    }
}