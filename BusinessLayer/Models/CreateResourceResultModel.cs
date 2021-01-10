using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateResourceResultModel
    {
        public bool Succeed { get; private set; } = true;

        public string Uri { get; set; }

        public IList<string> Errors { get; } = new List<string>();

        public string ObjectId { get; set; }

        public void AddError(string error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}