namespace BusinessLayer.Interfaces.Validators
{
    public interface IValidationUnitOfWork
    {
        ICreateCollectionModelValidator CreateCollectionModelValidator { get; }

        IUpdateCollectionModelValidator UpdateCollectionModelValidator { get; }

        ICreateItemModelValidator CreateItemModelValidator { get; }

        IUpdateItemModelValidator UpdateItemModelValidator { get; }

        ICreateDefaultOptionalFieldModelValidator CreateDefaultOptionalFieldModelValidator { get; }

        IDeleteOptionalFieldModelValidator DeleteOptionalFieldModelValidator { get; }
    }
}