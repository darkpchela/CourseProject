using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public class UpdateItemModelValidator : DefaultValidator<UpdateItemModel>, IUpdateItemModelValidator
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        public UpdateItemModelValidator(IHttpContextAccessor httpContextAccessor, IItemsCrudService itemsCrudService,
            ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService, IResourceCrudService resourceCrudService) : base(httpContextAccessor)
        {
            this.itemsCrudService = itemsCrudService;
            this.collectionsCrudService = collectionsCrudService;
            this.userCrudService = userCrudService;
            this.resourceCrudService = resourceCrudService;
        }

        protected async override Task BaseValidation(UpdateItemModel model)
        {
            var item = await itemsCrudService.GetAsync(model.ItemId);
            if (item is null)
                result.AddError("Item not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                result.AddError("Collection not found");
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not found");
            var resource = await resourceCrudService.GetAsync(model.ResourceId);
            if (resource is null)
                result.AddError("Resource not found");
        }

        protected async override Task OptionalValidation(UpdateItemModel model)
        {
            Authenticate(model);
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (!collection.OptionalFields.Select(of => of.Id).OrderBy(i => i).SequenceEqual(
                model.OptionalFields.Select(of => of.OptionalFieldId).OrderBy(i => i)))
                result.AddError("Optional fields matching error");
        }
    }
}