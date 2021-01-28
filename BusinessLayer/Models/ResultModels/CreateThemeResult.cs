namespace BusinessLayer.Models.ResultModels
{
    public class CreateThemeResult : ResultModel<string>
    {
        public int CreatedThemeId { get; set; }

        public string CreatedThemeName { get; set; }

        public CreateThemeResult()
        {
        }

        public CreateThemeResult(ResultModel<string> resultModel) : base(resultModel)
        {
        }
    }
}