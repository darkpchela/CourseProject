namespace BusinessLayer.Interfaces.Authenticators
{
    public interface IModelAuthenticatorsStore
    {
        ICreateCollectionModelAuthenticator CreateCollectionModelAuthenticator { get; }

        ICreateItemModelAuthenticator CreateItemModelAuthenticator { get; }

        ICreateOptionalFieldModelAuthenticator CreateOptionalFieldModelAuthenticator { get; }

        IDeleteCollectionModelAuthenticator DeleteCollectionModelAuthenticator { get; }

        IDeleteItemModelAuthenticator DeleteItemModelAuthenticator { get; }

        IDeleteOptionalFieldModelAuthenticator DeleteOptionalFieldModelAuthenticator { get; }

        IUpdateCollectionModelAuthenticator UpdateCollectionModelAuthenticator { get; }

        IUpdateItemModelAuthenticator UpdateItemModelAuthenticator { get; }
    }
}