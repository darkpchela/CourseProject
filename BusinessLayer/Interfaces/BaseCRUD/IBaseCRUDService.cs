using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface IBaseCrudService<TEntity, TModel> where TEntity : class where TModel : class
    {
        Task CreateAsync(TModel model);

        Task<TModel> GetAsync(int id);

        Task<IEnumerable<TModel>> GetAllAsync();

        Task Update(TModel model);

        Task DeleteAsync(int id);
    }
}