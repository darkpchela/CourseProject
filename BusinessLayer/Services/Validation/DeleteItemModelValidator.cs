using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class DeleteItemModelValidator : DefaultValidator<DeleteItemModel>, IDeleteItemModelValidator
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly IUserCrudService userCrudService;

        public DeleteItemModelValidator(IItemsCrudService itemsCrudService)
        {
            this.itemsCrudService = itemsCrudService;
        }

        protected async override Task BaseValidation(DeleteItemModel model)
        {
            var item = await itemsCrudService.GetAsync(model.ItemId);
            if (item is null)
                result.AddError("Item not found");
            var owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                result.AddError("User not found");
        }

        protected async override Task OptionalValidation(DeleteItemModel model)
        {
            var owner = await userCrudService.GetAsync(model.OwnerId);
        }
    }
}