using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IResourcesManager
    {
        Task<CreateResourceResultModel> CreateAsync(CreateResourceModel createResourceModel);

        Task DeleteAsync(ResourceModel resourceModel);
    }
}