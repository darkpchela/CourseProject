using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication
{
    public class CreateCollectionModelAuthenticator : DefaultAuthenticator<CreateCollectionModel>, ICreateCollectionModelAuthenticator
    {
        public CreateCollectionModelAuthenticator(IHttpContextAccessor httpContextAcssesor) : base(httpContextAcssesor)
        {
        }

        protected async override Task<bool> TrySetOwner(CreateCollectionModel model)
        {
            return true;
        }
    }
}