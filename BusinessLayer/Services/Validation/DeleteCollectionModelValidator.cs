using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class DeleteCollectionModelValidator : DefaultValidator<DeleteCollectionModel>, IDeleteCollectionModelValidator
    {
        private UserModel owner;

        private CollectionModel collection;

        private readonly IUserCrudService userCrudService;

        private readonly ICollectionsCrudService collectionsCrudService;

        public DeleteCollectionModelValidator(IUserCrudService userCrudService, ICollectionsCrudService collectionsCrudService)
        {
            this.userCrudService = userCrudService;
            this.collectionsCrudService = collectionsCrudService;
        }

        protected async override Task BaseValidation(DeleteCollectionModel model)
        {
            owner = await userCrudService.GetAsync(model.OwnerId);
            if (owner is null)
                ValidationResult.AddError("Owner not found");
            collection = await collectionsCrudService.GetAsync(model.CollectionId);
            if (collection is null)
                ValidationResult.AddError("Collection not found");
        }

        protected async override Task OptionalValidation(DeleteCollectionModel model)
        {
            if (!owner.Collections.Any(c => c.Id == model.CollectionId))
                ValidationResult.AddError("Owner matching error");
        }
    }
}