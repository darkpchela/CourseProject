using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IItemsManager
    {
        Task CreateAsync(CreateItemModel createItemModel);
    }
}