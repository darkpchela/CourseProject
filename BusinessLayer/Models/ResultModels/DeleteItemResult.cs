namespace BusinessLayer.Models.ResultModels
{
    public class DeleteItemResult : ResultModel<string>
    {
        public DeleteItemResult(ResultModel<string> result) : base(result)
        {
        }
    }
}