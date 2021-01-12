using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public abstract class ResultModel<TError>
    {
        public bool Succeed { get; private set; } = true;

        public IList<TError> Errors { get; } = new List<TError>();

        public void AddError(TError error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}