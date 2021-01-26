namespace BusinessLayer.Models.ResultModels
{
    public class AppSignInResult : ResultModel<string>
    {
        public AppSignInResult()
        {
        }

        public AppSignInResult(ResultModel<string> result) : base(result)
        {
        }
    }
}