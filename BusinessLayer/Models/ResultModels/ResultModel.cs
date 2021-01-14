using System.Collections.Generic;

namespace BusinessLayer.Models.ResultModels
{
    public abstract class ResultModel<TError>
    {
        public bool Succeed { get; private set; } = true;

        public IList<TError> Errors { get; } = new List<TError>();

        public ResultModel()
        {
        }

        public ResultModel(ResultModel<TError> model)
        {
            Succeed = model.Succeed;
            Errors = model.Errors;
        }

        public void AddError(TError error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}