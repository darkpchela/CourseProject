using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollectionsManager
    {
        Task<CreateCollectionResultModel> CreateAsync(CreateCollectionModel model);
    }
}