namespace BusinessLayer.Models.ResultModels
{
    public class CommentItemResult : ResultModel<string>
    {
        public string UserName { get; set; }

        public string Value { get; set; }

        public CommentItemResult()
        {
        }

        public CommentItemResult(ResultModel<string> result) : base(result)
        {
        }
    }
}