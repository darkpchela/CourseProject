using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollectionsManager
    {
        Task CreateAsync(CreateCollectionModel model);
    }
}