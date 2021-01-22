using BusinessLayer.Interfaces.Validation;

namespace BusinessLayer.Services.Validation
{
    public class ModelValidatorsStore : IModelValidatorsStore
    {
        public ICreateCollectionModelValidator CreateCollectionModelValidator { get; }

        public IUpdateCollectionModelValidator UpdateCollectionModelValidator { get; }

        public ICreateItemModelValidator CreateItemModelValidator { get; }

        public IUpdateItemModelValidator UpdateItemModelValidator { get; }

        public ICreateDefaultOptionalFieldModelValidator CreateDefaultOptionalFieldModelValidator { get; }

        public IDeleteOptionalFieldModelValidator DeleteOptionalFieldModelValidator { get; }

        public IDeleteCollectionModelValidator DeleteCollectionModelValidator { get; }

        public IDeleteItemModelValidator DeleteItemModelValidator { get; }

        public ILikeItemModelValidator LikeItemModelValidator { get; }

        public ICommentItemModelValidator CommentItemModelValidator { get; }

        public ModelValidatorsStore(ICreateCollectionModelValidator createCollectionModelValidator, IUpdateCollectionModelValidator updateCollectionModelValidator,
            ICreateItemModelValidator createItemModelValidator, IUpdateItemModelValidator updateItemModelValidator,
            ICreateDefaultOptionalFieldModelValidator createDefaultOptionalFieldModelValidator, IDeleteOptionalFieldModelValidator deleteOptionalFieldModelValidator, IDeleteItemModelValidator deleteItemModelValidator,
            IDeleteCollectionModelValidator deleteCollectionModelValidator, ILikeItemModelValidator likeItemModelValidator, ICommentItemModelValidator commentItemModelValidator)
        {
            this.CreateCollectionModelValidator = createCollectionModelValidator;
            this.UpdateCollectionModelValidator = updateCollectionModelValidator;
            this.CreateItemModelValidator = createItemModelValidator;
            this.UpdateItemModelValidator = updateItemModelValidator;
            this.CreateDefaultOptionalFieldModelValidator = createDefaultOptionalFieldModelValidator;
            this.DeleteOptionalFieldModelValidator = deleteOptionalFieldModelValidator;
            this.DeleteItemModelValidator = deleteItemModelValidator;
            this.DeleteCollectionModelValidator = deleteCollectionModelValidator;
            this.LikeItemModelValidator = likeItemModelValidator;
            this.CommentItemModelValidator = commentItemModelValidator;
        }
    }
}