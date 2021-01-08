using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface IUserCrudService : IBaseCRUDService<User, UserModel>
    {
    }
}