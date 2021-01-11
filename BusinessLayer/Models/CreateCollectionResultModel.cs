using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateCollectionResultModel
    {
        public bool Succeed { get; private set; } = true;

        public int CollectionId { get; set; }

        public IList<string> Errors { get; } = new List<string>();

        public void AddError(string error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}