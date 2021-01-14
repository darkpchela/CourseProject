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
    public class OptionalFieldsManager : IOptionalFieldsManager
    {
        private readonly IMapper mapper;

        private readonly IOptionalFieldsCrudService optionalFieldsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IFieldTypesCrudService fieldTypesCrudService;

        private readonly IUserCrudService userCrudService;

        public OptionalFieldsManager(IMapper mapper, IOptionalFieldsCrudService optionalFieldsCrudService, ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService, IFieldTypesCrudService fieldTypesCrudService)
        {
            this.optionalFieldsCrudService = optionalFieldsCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.fieldTypesCrudService = fieldTypesCrudService;
            this.userCrudService = userCrudService;
        }

        public async Task<CreateOptionalFieldResult> CreateDefaultAsync(CreateDefaultOptionalFieldModel createDefaultFieldModel)
        {
            var result = await ValidateCreateDefaultFieldModel(createDefaultFieldModel);
            if (!result.Succeed)
                return result;
            var model = new OptionalFieldModel
            {
                CollectionId = createDefaultFieldModel.CollectionId,
                Name = "Unnamed",
                TypeId = (await fieldTypesCrudService.GetAllAsync()).First().Id
            };
            await optionalFieldsCrudService.CreateAsync(model);
            result.OptionalFieldModel = model;
            return result;
        }

        public async Task<DeleteOptionalFieldResult> DeleteAsync(DeleteOptionalFieldModel deleteFieldModel)
        {
            var result = await ValidateDeleteOptionalFieldModel(deleteFieldModel);
            if (!result.Succeed)
                return result;
            await optionalFieldsCrudService.DeleteAsync(deleteFieldModel.OptionalFieldId);
            return result;
        }

        private async Task<CreateOptionalFieldResult> ValidateCreateDefaultFieldModel(CreateDefaultOptionalFieldModel model)
        {
            var result = new CreateOptionalFieldResult();
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var type = (await fieldTypesCrudService.GetAllAsync()).FirstOrDefault();
            if (type is null)
                result.AddError("Available field types not found ");
            if (!result.Succeed)
                return result;

            //if (!model.IsAdminRequest && collection.OwnerId != owner.Id)
            //    result.AddError("Access denied");
            return result;
        }

        private async Task<DeleteOptionalFieldResult> ValidateDeleteOptionalFieldModel(DeleteOptionalFieldModel model)
        {
            var result = new DeleteOptionalFieldResult();
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var field = await optionalFieldsCrudService.GetAsync(model.OptionalFieldId);
            if (field is null)
                result.AddError("Optional field not found");
            if (!result.Succeed)
                return result;

            if (!collection.OptionalFields.Any(f => f.Id == field.Id))
                result.AddError("Optional field not found");
            //if (!model.IsAdminRequest && collection.OwnerId != owner.Id)
            //    result.AddError("Access denied");
            return result;
        }
    }
}