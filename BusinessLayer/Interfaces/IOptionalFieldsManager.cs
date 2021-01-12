using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOptionalFieldsManager
    {
        Task<CreateOptionalFieldResultModel> CreateDefaultAsync(int collectionId);
    }
}