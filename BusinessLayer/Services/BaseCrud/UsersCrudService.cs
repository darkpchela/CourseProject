using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

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
    }
}