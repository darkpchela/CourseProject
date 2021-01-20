using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface IBaseCrudService<TEntity, TModel> where TEntity : class where TModel : class
    {
        Task CreateAsync(TModel model);

        Task CreateRangeAsync(IEnumerable<TModel> models);

        Task<TModel> GetAsync(int id);

        Task<IEnumerable<TModel>> GetAllAsync();

        Task UpdateAsync(TModel model);

        Task DeleteAsync(int id);
    }
}