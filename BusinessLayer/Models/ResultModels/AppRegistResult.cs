using Microsoft.AspNetCore.Identity;

namespace BusinessLayer.Models.ResultModels
{
    public class AppRegistResult : ResultModel<string>
    {
        public AppRegistResult()
        {
        }

        public AppRegistResult(ResultModel<string> result) : base(result)
        {
        }

        public AppRegistResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                Errors.Add(error.Description);
            }
        }
    }
}