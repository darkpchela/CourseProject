namespace BusinessLayer.Models.ResultModels
{
    public class CreateItemResult : ResultModel<string>
    {
        public int CreatedItemId { get; set; }

        public CreateItemResult()
        {
        }

        public CreateItemResult(ResultModel<string> result) : base(result)
        {
        }
    }
}