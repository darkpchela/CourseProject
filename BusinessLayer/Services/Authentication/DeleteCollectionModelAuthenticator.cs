using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication
{
    public class DeleteCollectionModelAuthenticator : DefaultAuthenticator<DeleteCollectionModel>, IDeleteCollectionModelAuthenticator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        public DeleteCollectionModelAuthenticator(IHttpContextAccessor httpContextAccessor, ICollectionsCrudService collectionsCrudService) : base(httpContextAccessor)
        {
            this.collectionsCrudService = collectionsCrudService;
        }

        protected async override Task<bool> TrySetOwner(DeleteCollectionModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                return false;
            model.OwnerId = collection.OwnerId;
            return true;
        }
    }
}