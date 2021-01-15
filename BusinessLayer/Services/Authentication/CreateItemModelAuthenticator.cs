using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication
{
    public class CreateItemModelAuthenticator : DefaultAuthenticator<CreateItemModel>, ICreateItemModelAuthenticator
    {
        public CreateItemModelAuthenticator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        protected async override Task<bool> TrySetOwner(CreateItemModel model)
        {
            return true;
        }
    }
}