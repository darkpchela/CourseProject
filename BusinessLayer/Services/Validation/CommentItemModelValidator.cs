using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class CommentItemModelValidator : DefaultValidator<CommentItemModel>, ICommentItemModelValidator
    {
        private readonly IUserCrudService userCrudService;

        public CommentItemModelValidator(IUserCrudService userCrudService)
        {
            this.userCrudService = userCrudService;
        }

        protected async override Task BaseValidation(CommentItemModel model)
        {
            var user = await userCrudService.GetAsync(model.UserId);
            if (user is null)
                ValidationResult.AddError("User not found");
            if (model.Value.Length > 512 || string.IsNullOrWhiteSpace(model.Value))
                ValidationResult.AddError("Value out of range");
        }

        protected async override Task OptionalValidation(CommentItemModel model)
        {
            return;
        }
    }
}