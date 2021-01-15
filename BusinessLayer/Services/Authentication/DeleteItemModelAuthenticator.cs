using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication
{
    public class DeleteItemModelAuthenticator : DefaultAuthenticator<DeleteItemModel>, IDeleteItemModelAuthenticator
    {
        private readonly IItemsCrudService itemsCrudService;

        public DeleteItemModelAuthenticator(IHttpContextAccessor httpContextAccessor, IItemsCrudService itemsCrudService) : base(httpContextAccessor)
        {
            this.itemsCrudService = itemsCrudService;
        }

        protected async override Task<bool> TrySetOwner(DeleteItemModel model)
        {
            var item = await itemsCrudService.GetAsync(model.ItemId);
            if (item is null)
                return false;
            model.OwnerId = item.OwnerId;
            return true;
        }
    }
}