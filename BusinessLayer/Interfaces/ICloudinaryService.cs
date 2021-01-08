using BusinessLayer.Models;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICloudinaryService
    {
        Task<UploadResultModel> UploadAsync(FileDescription fileDescription);

        Task<GetResourceModel> GetUri(string publicId);

        Task DeleteAsync(string publicId);
    }
}