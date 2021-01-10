using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateCollectionResultModel
    {
        public bool Succeed { get; set; }

        public int CollectionId { get; set; }

        public IList<string> Errors { get; set; } = new List<string>();
    }
}