namespace BusinessLayer.Models.ResultModels
{
    public class CreateResourceResult : ResultModel<string>
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string PublicId { get; set; }
    }
}