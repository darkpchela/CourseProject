using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IItemsManager
    {
        Task<CreateItemResult> CreateAsync(CreateItemModel createItemModel);

        Task<UpdateItemResult> UpdateAsync(UpdateItemModel updateItemModel);

        Task<DeleteItemResult> DeleteAsync(DeleteItemModel deleteItemModel);

        Task<IEnumerable<ItemModel>> SearchAsync(string text);
    }
}