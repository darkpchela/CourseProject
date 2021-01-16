using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class UpdateItemModelValidator : DefaultValidator<UpdateItemModel>, IUpdateItemModelValidator
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly IResourceCrudService resourceCrudService;

        public UpdateItemModelValidator(IItemsCrudService itemsCrudService, ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService, IResourceCrudService resourceCrudService)
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
                ValidationResult.AddError("Item not found");
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                ValidationResult.AddError("Collection not found");
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                ValidationResult.AddError("User not found");
            var resource = await resourceCrudService.GetAsync(model.ResourceId);
            if (resource is null)
                ValidationResult.AddError("Resource not found");
        }

        protected async override Task OptionalValidation(UpdateItemModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (!collection.OptionalFields.Select(of => of.Id).OrderBy(i => i).SequenceEqual(
                model.OptionalFields.Select(of => of.OptionalFieldId).OrderBy(i => i)))
                ValidationResult.AddError("Optional fields matching error");
        }
    }
}