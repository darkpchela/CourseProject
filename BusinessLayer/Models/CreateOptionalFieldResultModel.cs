using BusinessLayer.Models.DALModels;
using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class CreateOptionalFieldResultModel
    {
        public bool Succeed { get; private set; } = true;

        public IList<string> Errors { get; } = new List<string>();

        public OptionalFieldModel OptionalFieldModel { get; set; }

        public void AddError(string error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}