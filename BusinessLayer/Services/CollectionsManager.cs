using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollectionsManager : ICollectionsManager
    {
        private readonly ICPUnitOfWork cPUnitOfWork;

        private readonly IMapper mapper;

        public CollectionsManager(ICPUnitOfWork cPUnitOfWork, IMapper mapper)
        {
            this.cPUnitOfWork = cPUnitOfWork;
            this.mapper = mapper;
        }

        public async Task<CreateCollectionResultModel> CreateAsync(CreateCollectionModel createCollectionModel)
        {
            var result = new CreateCollectionResultModel();
            if (createCollectionModel is null)
            {
                result.Error = "Model is null";
                return result;
            }
            var collection = GetCollectionEntity(createCollectionModel);
            if (collection.Creator is null || collection.Theme is null)
            {
                result.Error = "Invalid input parameters";
                return result;
            }
            await cPUnitOfWork.CollectionsRepository.Add(collection);
            await SetCollectionFields(collection, createCollectionModel.Fields);
            await cPUnitOfWork.SaveChangesAsync();
            result.Succeed = true;
            result.CollectionId = collection.Id;
            return result;
        }

        private Collection GetCollectionEntity(CreateCollectionModel createCollectionModel)
        {
            var collection = mapper.Map<Collection>(createCollectionModel);
            collection.Creator = cPUnitOfWork.UsersRepository.GetAll().FirstOrDefault(u => u.Username == createCollectionModel.Owner);
            collection.Theme = cPUnitOfWork.ThemesRepository.GetAll().FirstOrDefault(t => t.Id == createCollectionModel.ThemeId);
            return collection;
        }

        private async Task SetCollectionFields(Collection collection, IEnumerable<OptionalFieldModel> optionalFieldModels)
        {
            var fields = mapper.Map<IEnumerable<OptionalField>>(optionalFieldModels);
            foreach (OptionalField field in fields)
            {
                var collectionField = new CollectionOptionalField
                {
                    Collection = collection,
                    OptionalField = field
                };
                await cPUnitOfWork.CollectionOptionalFieldRepository.Add(collectionField);
            }
        }
    }
}