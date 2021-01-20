using BusinessLayer.Models.DALModels;
using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public class CreateTagsResult : ResultModel<string>
    {
        public IEnumerable<TagModel> Created { get; set; }
    }
}