using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface IUserCrudService : IBaseCrudService<User, UserModel>
    {
        Task<UserModel> GetAsync(string username);
    }
}