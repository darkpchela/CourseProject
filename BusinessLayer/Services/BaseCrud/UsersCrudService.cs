using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BusinessLayer.Services.BaseCrud
{
    public class UsersCrudService : BaseCrudService<User, UserModel>, IUserCrudService
    {
        protected override IRepository<User> BaseRepository
        {
            get
            {
                return cPUnitOfWork.UsersRepository;
            }
        }

        public UsersCrudService(ICPUnitOfWork cPUnitOfWork, IMapper mapper) : base(cPUnitOfWork, mapper)
        {
        }

        public async Task<UserModel> GetAsync(string username)
        {
            var user = await BaseRepository.GetAll().FirstOrDefaultAsync(u => u.Username == username);
            var model = mapper.Map<UserModel>(user);
            return model;
        }
    }
}