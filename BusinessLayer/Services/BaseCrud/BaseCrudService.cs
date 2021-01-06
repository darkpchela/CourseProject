using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services.BaseCrud
{
    public abstract class BaseCRUDService<TEntity, TModel> : IBaseCRUDService<TEntity, TModel> where TEntity : class where TModel : class
    {
        protected readonly ICPUnitOfWork cPUnitOfWork;

        protected readonly IMapper mapper;

        protected abstract IRepository<TEntity> BaseRepository { get; }

        public BaseCRUDService(ICPUnitOfWork cPUnitOfWork, IMapper mapper)
        {
            this.cPUnitOfWork = cPUnitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(TModel model)
        {
            if (model is null)
                return;
            var entity = mapper.Map<TEntity>(model);
            await BaseRepository.Create(entity);
            await cPUnitOfWork.SaveChangesAsync();
            mapper.Map(entity, model);
        }

        public async Task DeleteAsync(int id)
        {
            await BaseRepository.Delete(id);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAll().ToListAsync();
            var models = mapper.Map<IEnumerable<TModel>>(entities);
            return models;
        }

        public async Task<TModel> GetAsync(int id)
        {
            var entity = await BaseRepository.Get(id);
            var model = mapper.Map<TModel>(entity);
            return model;
        }

        public Task Update(TModel model)
        {
            var entity = mapper.Map<TEntity>(model);
            BaseRepository.Update(entity);
            return Task.CompletedTask;
        }
    }
}