namespace BusinessLayer.Models.ResultModels
{
    public class CreateItemResult : ResultModel<string>
    {
        public int ItemId { get; set; }

        public CreateItemResult()
        {
        }

        public CreateItemResult(ResultModel<string> result) : base(result)
        {
        }
    }
}