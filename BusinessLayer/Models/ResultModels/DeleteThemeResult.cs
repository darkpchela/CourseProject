namespace BusinessLayer.Models.ResultModels
{
    public class DeleteThemeResult : ResultModel<string>
    {
        public int DeletedThemeId { get; set; }

        public DeleteThemeResult()
        {
        }

        public DeleteThemeResult(ResultModel<string> result) : base(result)
        {
        }
    }
}