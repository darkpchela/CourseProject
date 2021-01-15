using BusinessLayer.Interfaces.Authenticators;

namespace BusinessLayer.Services.Authenticators
{
    public class ModelAuthenticatorsStore : IModelAuthenticatorsStore
    {
        public ICreateCollectionModelAuthenticator CreateCollectionModelAuthenticator { get; set; }

        public ICreateItemModelAuthenticator CreateItemModelAuthenticator { get; set; }

        public ICreateOptionalFieldModelAuthenticator CreateOptionalFieldModelAuthenticator { get; set; }

        public IDeleteCollectionModelAuthenticator DeleteCollectionModelAuthenticator { get; set; }

        public IDeleteItemModelAuthenticator DeleteItemModelAuthenticator { get; set; }

        public IDeleteOptionalFieldModelAuthenticator DeleteOptionalFieldModelAuthenticator { get; set; }

        public IUpdateCollectionModelAuthenticator UpdateCollectionModelAuthenticator { get; set; }

        public IUpdateItemModelAuthenticator UpdateItemModelAuthenticator { get; set; }

        public ModelAuthenticatorsStore(ICreateCollectionModelAuthenticator createCollectionModelAuthenticator, ICreateItemModelAuthenticator createItemModelAuthenticator,
            ICreateOptionalFieldModelAuthenticator createOptionalFieldModelAuthenticator, IDeleteCollectionModelAuthenticator deleteCollectionModelAuthenticator, IDeleteItemModelAuthenticator deleteItemModelAuthenticator,
            IDeleteOptionalFieldModelAuthenticator deleteOptionalFieldModelAuthenticator, IUpdateCollectionModelAuthenticator updateCollectionModelAuthenticator, IUpdateItemModelAuthenticator updateItemModelAuthenticator)
        {
            this.CreateCollectionModelAuthenticator = createCollectionModelAuthenticator;
            this.CreateItemModelAuthenticator = createItemModelAuthenticator;
            this.CreateOptionalFieldModelAuthenticator = createOptionalFieldModelAuthenticator;
            this.DeleteCollectionModelAuthenticator = deleteCollectionModelAuthenticator;
            this.DeleteItemModelAuthenticator = deleteItemModelAuthenticator;
            this.DeleteOptionalFieldModelAuthenticator = deleteOptionalFieldModelAuthenticator;
            this.UpdateCollectionModelAuthenticator = updateCollectionModelAuthenticator;
            this.UpdateItemModelAuthenticator = updateItemModelAuthenticator;
        }
    }
}