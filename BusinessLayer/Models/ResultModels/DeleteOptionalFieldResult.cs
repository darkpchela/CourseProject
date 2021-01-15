namespace BusinessLayer.Models.ResultModels
{
    public class DeleteOptionalFieldResult : ResultModel<string>
    {
        public DeleteOptionalFieldResult()
        {
        }

        public DeleteOptionalFieldResult(ResultModel<string> result) : base(result)
        {
        }
    }
}