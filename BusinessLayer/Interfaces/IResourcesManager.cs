using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IResourcesManager
    {
        Task<CreateResourceResult> CreateAsync(CreateResourceModel createResourceModel);

        Task DeleteAsync(ResourceModel resourceModel);

        Task DeleteAsync(int id);

        Task RemoveGarbageAsync();
    }
}