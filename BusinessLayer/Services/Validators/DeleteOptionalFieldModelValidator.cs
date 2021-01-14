using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public class DeleteOptionalFieldModelValidator : DefaultValidator<DeleteOptionalFieldModel>, IDeleteOptionalFieldModelValidator
    {
        private readonly IOptionalFieldsCrudService optionalFieldsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        public DeleteOptionalFieldModelValidator(IHttpContextAccessor httpContextAccessor, IOptionalFieldsCrudService optionalFieldsCrudService, ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService) : base(httpContextAccessor)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.userCrudService = userCrudService;
            this.optionalFieldsCrudService = optionalFieldsCrudService;
        }

        protected async override Task BaseValidation(DeleteOptionalFieldModel model)
        {
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var field = await optionalFieldsCrudService.GetAsync(model.OptionalFieldId);
            if (field is null)
                result.AddError("Optional field not found");
        }

        protected async override Task OptionalValidation(DeleteOptionalFieldModel model)
        {
            Authenticate(model);
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            var field = await optionalFieldsCrudService.GetAsync(model.OptionalFieldId);
            if (!collection.OptionalFields.Any(f => f.Id == field.Id))
                result.AddError("Optional field not found");
            if (model.OwnerId != collection.OwnerId)
                result.AddError("Owner matching error");
        }
    }
}