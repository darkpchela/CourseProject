namespace BusinessLayer.Interfaces.Validators
{
    public interface IModelValidatorsStore
    {
        ICreateCollectionModelValidator CreateCollectionModelValidator { get; }

        IUpdateCollectionModelValidator UpdateCollectionModelValidator { get; }

        ICreateItemModelValidator CreateItemModelValidator { get; }

        IUpdateItemModelValidator UpdateItemModelValidator { get; }

        ICreateDefaultOptionalFieldModelValidator CreateDefaultOptionalFieldModelValidator { get; }

        IDeleteOptionalFieldModelValidator DeleteOptionalFieldModelValidator { get; }

        IDeleteCollectionModelValidator DeleteCollectionModelValidator { get; }

        IDeleteItemModelValidator DeleteItemModelValidator { get; }
    }
}