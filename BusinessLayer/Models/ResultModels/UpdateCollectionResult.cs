namespace BusinessLayer.Models.ResultModels
{
    public class UpdateCollectionResult : ResultModel<string>
    {
        public UpdateCollectionResult()
        {
        }

        public UpdateCollectionResult(ResultModel<string> model) : base(model)
        {
        }
    }
}