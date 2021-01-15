namespace BusinessLayer.Models.ResultModels
{
    public class ModelAuthenticationResult : ResultModel<string>
    {
        public ModelAuthenticationResult() : base()
        {
        }

        public ModelAuthenticationResult(ResultModel<string> result) : base(result)
        {
        }
    }
}