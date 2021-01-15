using BusinessLayer.Interfaces.Validators;

namespace BusinessLayer.Services.Validators
{
    public class ModelValidatorsStore : IModelValidatorsStore
    {
        public ICreateCollectionModelValidator CreateCollectionModelValidator { get; set; }

        public IUpdateCollectionModelValidator UpdateCollectionModelValidator { get; set; }

        public ICreateItemModelValidator CreateItemModelValidator { get; set; }

        public IUpdateItemModelValidator UpdateItemModelValidator { get; set; }

        public ICreateDefaultOptionalFieldModelValidator CreateDefaultOptionalFieldModelValidator { get; set; }

        public IDeleteOptionalFieldModelValidator DeleteOptionalFieldModelValidator { get; set; }

        public IDeleteCollectionModelValidator DeleteCollectionModelValidator { get; set; }

        public IDeleteItemModelValidator DeleteItemModelValidator { get; set; }

        public ModelValidatorsStore(ICreateCollectionModelValidator createCollectionModelValidator, IUpdateCollectionModelValidator updateCollectionModelValidator,
            ICreateItemModelValidator createItemModelValidator, IUpdateItemModelValidator updateItemModelValidator,
            ICreateDefaultOptionalFieldModelValidator createDefaultOptionalFieldModelValidator, IDeleteOptionalFieldModelValidator deleteOptionalFieldModelValidator, IDeleteItemModelValidator deleteItemModelValidator,
            IDeleteCollectionModelValidator deleteCollectionModelValidator)
        {
            this.CreateCollectionModelValidator = createCollectionModelValidator;
            this.UpdateCollectionModelValidator = updateCollectionModelValidator;
            this.CreateItemModelValidator = createItemModelValidator;
            this.UpdateItemModelValidator = updateItemModelValidator;
            this.CreateDefaultOptionalFieldModelValidator = createDefaultOptionalFieldModelValidator;
            this.DeleteOptionalFieldModelValidator = deleteOptionalFieldModelValidator;
            this.DeleteItemModelValidator = deleteItemModelValidator;
            this.DeleteCollectionModelValidator = deleteCollectionModelValidator;
        }
    }
}