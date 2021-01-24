using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public class DeleteUsersResult : ResultModel<string>
    {
        public IList<int> DeletedUsers { get; } = new List<int>();

        public IList<int> NotDeleted { get; } = new List<int>();
    }
}