using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface ICollectionsCrudService : IBaseCrudService<Collection, CollectionModel>
    {
    }
}