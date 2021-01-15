using BusinessLayer.Interfaces.Authenticators;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authenticators
{
    public class UpdateItemModelAuthenticator : DefaultAuthenticator<UpdateItemModel>, IUpdateItemModelAuthenticator
    {
        private readonly IItemsCrudService itemsCrudService;

        public UpdateItemModelAuthenticator(IHttpContextAccessor httpContextAccessor, IItemsCrudService itemsCrudService) : base(httpContextAccessor)
        {
            this.itemsCrudService = itemsCrudService;
        }

        protected async override Task<bool> TrySetOwner(UpdateItemModel model)
        {
            var item = await itemsCrudService.GetAsync(model.ItemId);
            if (item is null)
                return false;
            model.OwnerId = item.OwnerId;
            return true;
        }
    }
}