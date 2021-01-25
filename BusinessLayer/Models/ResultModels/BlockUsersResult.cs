using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public class BlockUsersResult : ResultModel<string>
    {
        public IList<int> Blocked { get; } = new List<int>();

        public IList<int> NotBlocked { get; } = new List<int>();
    }
}