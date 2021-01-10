using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface IResourceCrudService : IBaseCrudService<Resource, ResourceModel>
    {
    }
}