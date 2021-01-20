using BusinessLayer.Models.DALModels;
using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public class FindTagsResult : ResultModel<string>
    {
        public IEnumerable<TagModel> Founded { get; set; }
    }
}