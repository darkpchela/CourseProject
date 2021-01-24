using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public class UnblockUsersResult : ResultModel<string>
    {
        public IList<int> Unblocked { get; } = new List<int>();

        public IList<int> NotUnblocked { get; } = new List<int>();
    }
}