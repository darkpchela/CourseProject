using BusinessLayer.Interfaces.Authenticators;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authenticators
{
    public class CreateOptionalFieldModelAuthenticator : DefaultAuthenticator<CreateDefaultOptionalFieldModel>, ICreateOptionalFieldModelAuthenticator
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        public CreateOptionalFieldModelAuthenticator(IHttpContextAccessor httpContextAccessor, ICollectionsCrudService collectionsCrudService) : base(httpContextAccessor)
        {
            this.collectionsCrudService = collectionsCrudService;
        }

        protected async override Task<bool> TrySetOwner(CreateDefaultOptionalFieldModel model)
        {
            var collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                return false;
            model.OwnerId = collection.OwnerId;
            return true;
        }
    }
}