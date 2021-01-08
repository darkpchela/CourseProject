using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICollectionsCrudService collectionsCrudService;

        private readonly IUserCrudService userCrudService;

        private readonly ICloudinaryService cloudinaryService;

        private readonly ICollectionOptionalFieldCrudService collectionOptionalFieldCrudService;

        public CollectionsManager(ICollectionsCrudService collectionsCrudService, IUserCrudService userCrudService, ICloudinaryService cloudinaryService,
            ICollectionOptionalFieldCrudService collectionOptionalFieldCrudService)
        {
            this.collectionsCrudService = collectionsCrudService;
            this.userCrudService = userCrudService;
            this.cloudinaryService = cloudinaryService;
            this.collectionOptionalFieldCrudService = collectionOptionalFieldCrudService;
        }

        public async Task CreateAsync(CreateCollectionModel model)
        {
           
        }
    }
}
