using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateItemResultModel
    {
        public bool Succeed { get; set; }

        public IList<string> Errors { get; } = new List<string>();
    }
}