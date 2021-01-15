namespace BusinessLayer.Models.ResultModels
{
    public class DeleteCollectionResult : ResultModel<string>
    {
        public DeleteCollectionResult(ResultModel<string> result) : base(result)
        {
        }
    }
}