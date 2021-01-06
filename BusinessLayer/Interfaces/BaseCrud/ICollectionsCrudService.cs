using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface ICollectionsCrudService : IBaseCRUDService<Collection, CollectionModel>
    {
    }
}