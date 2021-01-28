using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class CreateCollectionModelValidator : DefaultValidator<CreateCollectionModel>, ICreateCollectionModelValidator
    {
        private readonly IThemesCrudService themesCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        public CreateCollectionModelValidator(IThemesCrudService themesCrudService, IUserCrudService userCrudService, ICollectionsCrudService collectionsCrudService , IResourceCrudService resourceCrudService)
        {
            this.themesCrudService = themesCrudService;
            this.userCrudService = userCrudService;
            this.resourceCrudService = resourceCrudService;
            this.collectionsCrudService = collectionsCrudService;
        }

        protected async override Task BaseValidation(CreateCollectionModel model)
        {
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                ValidationResult.Errors.Add("User not found");
            var resource = await resourceCrudService.GetAsync(model.ResourceId);
            if (resource is null)
                ValidationResult.AddError("Resource not found");
            var theme = await themesCrudService.GetAsync(model.ThemeId);
            if (theme is null)
                ValidationResult.Errors.Add("Theme not found");
        }

        protected async override Task OptionalValidation(CreateCollectionModel model)
        {
            var collection = (await collectionsCrudService.GetAllAsync()).FirstOrDefault(c => c.Name == model.Name);
            if (collection != null)
                ValidationResult.AddError("Name already taken");
        }
    }
}