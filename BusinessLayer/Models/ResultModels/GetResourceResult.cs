namespace BusinessLayer.Models.ResultModels
{
    public class GetResourceResult : ResultModel<string>
    {
        public string Url { get; set; }
    }
}