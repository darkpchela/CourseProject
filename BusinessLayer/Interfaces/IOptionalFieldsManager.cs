using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOptionalFieldsManager
    {
        Task<CreateOptionalFieldResult> CreateDefaultAsync(int collectionId);
    }
}