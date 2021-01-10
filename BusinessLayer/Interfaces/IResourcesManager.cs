using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IResourcesManager
    {
        Task<CreateResourceResultModel> CreateAsync(CreateResourceModel createResourceModel);
    }
}