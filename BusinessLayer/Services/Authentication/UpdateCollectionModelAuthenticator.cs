using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication
{
    public class UpdateCollectionModelAuthenticator : DefaultAuthenticator<UpdateCollectionModel>, IUpdateCollectionModelAuthenticator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        public UpdateCollectionModelAuthenticator(IHttpContextAccessor httpContextAccessor, ICollectionsCrudService collectionsCrudService) : base(httpContextAccessor)
        {
            this.collectionsCrudService = collectionsCrudService;
        }

        protected async override Task<bool> TrySetOwner(UpdateCollectionModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                return false;
            model.OwnerId = collection.OwnerId;
            return true;
        }
    }
}