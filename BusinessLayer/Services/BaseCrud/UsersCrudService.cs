using AutoMapper;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public override async Task DeleteAsync(int id)
        {
            var entity = await BaseRepository.Get(id);
            entity.Collections.ToList().ForEach(c => cPUnitOfWork.CollectionsRepository.Delete(c.Id));
            entity.Items.ToList().ForEach(i => cPUnitOfWork.ItemsRepository.Delete(i.Id));
            entity.Comments.ToList().ForEach(c => cPUnitOfWork.CommentsRepository.Delete(c.Id));
            entity.ItemLikes.ToList().ForEach(il => cPUnitOfWork.ItemLikeRepository.Delete(il.Id));
            await BaseRepository.Delete(id);
            await cPUnitOfWork.SaveChangesAsync();
        }
    }
}