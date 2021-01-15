using BusinessLayer.Interfaces.Validators;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validators
{
    public class DeleteCollectionModelValidator : DefaultValidator<DeleteCollectionModel>, IDeleteCollectionModelValidator
    {
        protected override Task BaseValidation(DeleteCollectionModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task OptionalValidation(DeleteCollectionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
