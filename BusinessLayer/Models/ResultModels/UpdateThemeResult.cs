namespace BusinessLayer.Models.ResultModels
{
    public class UpdateThemeResult : ResultModel<string>
    {
        public int UpdatedThemeId;

        public string Name;

        public UpdateThemeResult()
        {
        }

        public UpdateThemeResult(ResultModel<string> result) : base(result)
        {
        }
    }
}