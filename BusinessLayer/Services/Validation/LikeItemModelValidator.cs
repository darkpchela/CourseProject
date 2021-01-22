using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class LikeItemModelValidator : DefaultValidator<LikeItemModel>, ILikeItemModelValidator
    {
        private readonly IUserCrudService userCrudService;

        private readonly IItemsCrudService itemsCrudService;

        public LikeItemModelValidator(IUserCrudService userCrudService, IItemsCrudService itemsCrudService)
        {
            this.userCrudService = userCrudService;
            this.itemsCrudService = itemsCrudService;
        }

        protected async override Task BaseValidation(LikeItemModel model)
        {
            var user = await userCrudService.GetAsync(model.UserId);
            if (user is null)
                ValidationResult.AddError("User not found");
            var item = await itemsCrudService.GetAsync(model.ItemId);
            if (item is null)
                ValidationResult.AddError("Item not found");
        }

        protected async override Task OptionalValidation(LikeItemModel model)
        {
            return;
        }
    }
}