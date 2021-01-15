using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class CreateItemModelValidator : DefaultValidator<CreateItemModel>, ICreateItemModelValidator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        public CreateItemModelValidator(ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService, IResourceCrudService resourceCrudService)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.userCrudService = userCrudService;
            this.resourceCrudService = resourceCrudService;
        }

        protected async override Task BaseValidation(CreateItemModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not exists");
            var resource = await resourceCrudService.GetAsync(model.ResourceId);
            if (resource is null)
                result.AddError("Resource not found");
        }

        protected async override Task OptionalValidation(CreateItemModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (!collection.OptionalFields.Select(of => of.Id).OrderBy(i => i).SequenceEqual(
                model.OptionalFields.Select(of => of.OptionalFieldId).OrderBy(i => i)))
                result.AddError("Optional fields matching error");
        }
    }
}