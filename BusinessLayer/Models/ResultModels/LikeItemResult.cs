using BusinessLayer.Etc;

namespace BusinessLayer.Models.ResultModels
{
    public class LikeItemResult : ResultModel<string>
    {
        public LikeResults Result { get; set; }

        public LikeItemResult()
        {
        }

        public LikeItemResult(ResultModel<string> result) : base(result)
        {
        }
    }
}