using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public class DeleteItemModelValidator : DefaultValidator<DeleteItemModel>
    {
        private readonly IItemsCrudService itemsCrudService;

        private readonly IUserCrudService userCrudService;

        public DeleteItemModelValidator(IHttpContextAccessor httpContextAccessor, IItemsCrudService itemsCrudService) : base(httpContextAccessor)
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
            Authenticate(model);
            var owner = await userCrudService.GetAsync(model.OwnerId);
        }
    }
}
