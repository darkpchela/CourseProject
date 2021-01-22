namespace BusinessLayer.Models.ResultModels
{
    public class CommentItemResult : ResultModel<string>
    {
        public CommentItemResult()
        {
        }

        public CommentItemResult(ResultModel<string> result) : base(result)
        {
        }
    }
}