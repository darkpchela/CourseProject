using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces.BaseCrud
{
    public interface ITagsCrudService : IBaseCRUDService<Tag, TagModel>
    {
    }
}