namespace BusinessLayer.Models.ResultModels
{
    public class CreateItemResult : ResultModel<string>
    {
        public int ItemId { get; set; }
    }
}