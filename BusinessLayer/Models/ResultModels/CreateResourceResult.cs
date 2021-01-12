using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public class CreateResourceResult
    {
        public int Id { get; set; }

        public bool Succeed { get; private set; } = true;

        public string Url { get; set; }

        public IList<string> Errors { get; } = new List<string>();

        public string PublicId { get; set; }

        public void AddError(string error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}