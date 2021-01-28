using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class UpdateCollectionModelValidator : DefaultValidator<UpdateCollectionModel>, IUpdateCollectionModelValidator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IThemesCrudService themesCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        public UpdateCollectionModelValidator(ICollectionsCrudService collectionsCrudService, IThemesCrudService themesCrudService, IUserCrudService userCrudService, IResourceCrudService resourceCrudService)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.themesCrudService = themesCrudService;
            this.userCrudService = userCrudService;
            this.resourceCrudService = resourceCrudService;
        }

        protected override async Task BaseValidation(UpdateCollectionModel model)
        {
            var collections = await collectionsCrudService.GetAllAsync();
            if (!collections.Any(c => c.Id == model.CollectionId))
                ValidationResult.AddError("Collection not found");
            if (collections.Any(c => c.Name == model.Name && c.Id != model.CollectionId))
                ValidationResult.AddError("Collection with such name already exists");
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                ValidationResult.AddError("User not found");
            var resource = await resourceCrudService.GetAsync(model.ResourceId);
            if (resource is null)
                ValidationResult.AddError("Resource not found");
            var theme = await themesCrudService.GetAsync(model.ThemeId);
            if (theme is null)
                ValidationResult.AddError("Theme not found");
        }

        protected async override Task OptionalValidation(UpdateCollectionModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (model.OwnerId != collection.OwnerId)
                ValidationResult.AddError("Owner matching error");
            if (!collection.OptionalFields.Select(of => of.Id).OrderBy(i => i).SequenceEqual(model.OptionalFields.Select(of => of.Id).OrderBy(i => i)))
                ValidationResult.AddError("Optional fields matching error");
        }
    }
}