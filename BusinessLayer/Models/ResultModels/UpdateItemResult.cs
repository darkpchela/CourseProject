namespace BusinessLayer.Models.ResultModels
{
    public class UpdateItemResult : ResultModel<string>
    {
        public UpdateItemResult(ResultModel<string> result) : base(result)
        {
        }
    }
}