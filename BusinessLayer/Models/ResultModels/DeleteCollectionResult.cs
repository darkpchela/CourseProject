namespace BusinessLayer.Models.ResultModels
{
    public class DeleteCollectionResult : ResultModel<string>
    {
        public DeleteCollectionResult()
        {
        }

        public DeleteCollectionResult(ResultModel<string> result) : base(result)
        {
        }
    }
}