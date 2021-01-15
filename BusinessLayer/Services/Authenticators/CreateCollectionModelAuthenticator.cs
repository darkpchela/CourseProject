using BusinessLayer.Interfaces.Authenticators;
using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authenticators
{
    public class CreateCollectionModelAuthenticator : ICreateCollectionModelAuthenticator
    {
        private readonly 

        public Task<ModelAuthenticationResult> Authenticate(CreateCollectionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
