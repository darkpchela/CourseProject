using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public class CreateCollectionModelValidator : DefaultValidator<CreateCollectionModel>, ICreateCollectionModelValidator
    {
        private readonly IThemesCrudService themesCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        public CreateCollectionModelValidator(IHttpContextAccessor httpContextAccessor, IThemesCrudService themesCrudService, IUserCrudService userCrudService, IResourceCrudService resourceCrudService) : base(httpContextAccessor)
        {
            this.themesCrudService = themesCrudService;
            this.userCrudService = userCrudService;
            this.resourceCrudService = resourceCrudService;
        }

        protected async override Task BaseValidation(CreateCollectionModel model)
        {
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.Errors.Add("User not found");
            var resource = await resourceCrudService.GetAsync(model.ResourceId);
            if (resource is null)
                result.AddError("Resource not found");
            var theme = await themesCrudService.GetAsync(model.ThemeId);
            if (theme is null)
                result.Errors.Add("Theme not found");
        }

        protected async override Task OptionalValidation(CreateCollectionModel model)
        {
            Authenticate(model);
        }
    }
}