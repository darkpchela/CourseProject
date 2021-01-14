namespace BusinessLayer.Models.ResultModels
{
    public class CreateCollectionResult : ResultModel<string>
    {
        public int CollectionId { get; set; }

        public CreateCollectionResult(ResultModel<string> result) : base(result)
        {
        }
    }
}