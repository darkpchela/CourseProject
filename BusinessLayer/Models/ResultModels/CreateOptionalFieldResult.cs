using BusinessLayer.Models.DALModels;

namespace BusinessLayer.Models.ResultModels
{
    public class CreateOptionalFieldResult : ResultModel<string>
    {
        public OptionalFieldModel OptionalFieldModel { get; set; }

        public CreateOptionalFieldResult()
        {
        }

        public CreateOptionalFieldResult(ResultModel<string> result) : base(result)
        {
        }
    }
}