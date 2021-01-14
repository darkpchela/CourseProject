using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public class UpdateCollectionModelValidator : Validator<UpdateCollectionModel, UpdateCollectionResult>, IUpdateCollectionModelValidator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IThemesCrudService themesCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        public UpdateCollectionModelValidator(IHttpContextAccessor httpContextAccessor, ICollectionsCrudService collectionsCrudService, IThemesCrudService themesCrudService, IUserCrudService userCrudService, IResourceCrudService resourceCrudService) : base(httpContextAccessor)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.themesCrudService = themesCrudService;
            this.userCrudService = userCrudService;
            this.resourceCrudService = resourceCrudService;
        }

        protected override async Task BaseValidation(UpdateCollectionModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var resource = await resourceCrudService.GetAsync(model.ResourceId);
            if (resource is null)
                result.AddError("Resource not found");
            var theme = await themesCrudService.GetAsync(model.ThemeId);
            if (theme is null)
                result.AddError("Theme not found");
        }

        protected async override Task OptionalValidation(UpdateCollectionModel model)
        {
            Authenticate(model);
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (model.OwnerId != collection.OwnerId)
                result.AddError("Owner matching error");
            if (!collection.OptionalFields.Select(of => of.Id).OrderBy(i => i).SequenceEqual(model.OptionalFields.Select(of => of.Id).OrderBy(i => i)))
                result.AddError("Optional fields matching error");
        }
    }
}