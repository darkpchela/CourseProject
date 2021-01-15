using BusinessLayer.Interfaces.Authenticators;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authenticators
{
    public class DeleteOptionalFieldModelAuthenticator : DefaultAuthenticator<DeleteOptionalFieldModel>, IDeleteOptionalFieldModelAuthenticator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        public DeleteOptionalFieldModelAuthenticator(IHttpContextAccessor httpContextAccessor, ICollectionsCrudService collectionsCrudService) : base(httpContextAccessor)
        {
            this.collectionsCrudService = collectionsCrudService;
        }

        protected async override Task<bool> TrySetOwner(DeleteOptionalFieldModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                return false;
            model.OwnerId = collection.OwnerId;
            return true;
        }
    }
}