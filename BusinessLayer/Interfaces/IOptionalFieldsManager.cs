using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IOptionalFieldsManager
    {
        Task<CreateOptionalFieldResult> CreateAsync(CreateOptionalFieldModel createFieldModel);

        Task<DeleteOptionalFieldResult> DeleteAsync(DeleteOptionalFieldModel deleteFieldModel);
    }
}