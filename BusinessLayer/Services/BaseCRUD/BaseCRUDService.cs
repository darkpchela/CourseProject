using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services.BaseCrud
{
    public abstract class BaseCrudService<TEntity, TModel> : IBaseCrudService<TEntity, TModel> where TEntity : class where TModel : class
    {
        protected readonly ICPUnitOfWork cPUnitOfWork;

        protected readonly IMapper mapper;

        protected abstract IRepository<TEntity> BaseRepository { get; }

        public BaseCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper)
        {
            this.cPUnitOfWork = cPUnitOfWork;
            this.mapper = mapper;
        }

        public virtual async Task CreateAsync(TModel model)
        {
            if (model is null)
                return;
            var entity = mapper.Map<TEntity>(model);
            await BaseRepository.Add(entity);
            await cPUnitOfWork.SaveChangesAsync();
            mapper.Map(entity, model);
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TModel> models)
        {
            if (models is null)
                return;
            var entities = mapper.Map<IEnumerable<TEntity>>(models);
            foreach (var entity in entities)
            {
                await BaseRepository.Add(entity);
            }
            await cPUnitOfWork.SaveChangesAsync();
            mapper.Map(entities, models);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await BaseRepository.Delete(id);
            await cPUnitOfWork.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                await BaseRepository.Delete(id);
            }
            await cPUnitOfWork.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAll().ToListAsync();
            var models = mapper.Map<IEnumerable<TModel>>(entities);
            return models;
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            var entity = await BaseRepository.Get(id);
            var model = mapper.Map<TModel>(entity);
            return model;
        }

        public virtual async Task UpdateAsync(TModel model)
        {
            var entity = mapper.Map<TEntity>(model);
            BaseRepository.Update(entity);
            await cPUnitOfWork.SaveChangesAsync();
        }
    }
}