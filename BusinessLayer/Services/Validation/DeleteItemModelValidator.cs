using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class DeleteItemModelValidator : DefaultValidator<DeleteItemModel>, IDeleteItemModelValidator
    {
        private ItemModel item;

        private UserModel owner;

        private readonly IItemsCrudService itemsCrudService;

        private readonly IUserCrudService userCrudService;

        public DeleteItemModelValidator(IItemsCrudService itemsCrudService, IUserCrudService userCrudService)
        {
            this.itemsCrudService = itemsCrudService;
            this.userCrudService = userCrudService;
        }

        protected async override Task BaseValidation(DeleteItemModel model)
        {
            item = await itemsCrudService.GetAsync(model.ItemId);
            if (item is null)
                ValidationResult.AddError("Item not found");
            owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                ValidationResult.AddError("Owner not found");
        }

        protected async override Task OptionalValidation(DeleteItemModel model)
        {
            if (!owner.Items.Any(i => i.Id == model.ItemId))
                ValidationResult.AddError("Owner matching error");
        }
    }
}