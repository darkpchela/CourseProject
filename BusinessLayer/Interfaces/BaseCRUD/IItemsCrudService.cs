using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface IItemsCrudService : IBaseCrudService<Item, ItemModel>
    {
    }
}