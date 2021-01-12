using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public class UpdateCollectionResult
    {
        public bool Succeed { get; private set; } = true;

        public IList<string> Errors { get; } = new List<string>();

        public void AddError(string error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}