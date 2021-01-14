using BusinessLayer.Interfaces.Validators;

namespace BusinessLayer.Services.Validators
{
    public class ValidationUnitOfWork : IValidationUnitOfWork
    {
        public ICreateCollectionModelValidator CreateCollectionModelValidator { get; set; }

        public IUpdateCollectionModelValidator UpdateCollectionModelValidator { get; set; }

        public ICreateItemModelValidator CreateItemModelValidator { get; set; }

        public IUpdateItemModelValidator UpdateItemModelValidator { get; set; }

        public ICreateDefaultOptionalFieldModelValidator CreateDefaultOptionalFieldModelValidator { get; set; }

        public IDeleteOptionalFieldModelValidator DeleteOptionalFieldModelValidator { get; set; }

        public ValidationUnitOfWork(ICreateCollectionModelValidator createCollectionModelValidator, IUpdateCollectionModelValidator updateCollectionModelValidator,
            ICreateItemModelValidator createItemModelValidator, IUpdateItemModelValidator updateItemModelValidator,
            ICreateDefaultOptionalFieldModelValidator createDefaultOptionalFieldModelValidator, IDeleteOptionalFieldModelValidator deleteOptionalFieldModelValidator)
        {
            this.CreateCollectionModelValidator = createCollectionModelValidator;
            this.UpdateCollectionModelValidator = updateCollectionModelValidator;
            this.CreateItemModelValidator = createItemModelValidator;
            this.UpdateItemModelValidator = updateItemModelValidator;
            this.CreateDefaultOptionalFieldModelValidator = createDefaultOptionalFieldModelValidator;
            this.DeleteOptionalFieldModelValidator = deleteOptionalFieldModelValidator;
        }
    }
}