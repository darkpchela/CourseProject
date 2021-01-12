using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IItemsManager
    {
        Task<CreateItemResult> CreateAsync(CreateItemModel createItemModel);
    }
}