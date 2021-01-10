using CloudinaryDotNet.Actions;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICloudinaryService
    {
        Task<UploadResult> UploadAsync(string name, Stream fileStream);

        Task<GetResourceResult> GetUri(string publicId);

        Task DeleteAsync(string publicId);
    }
}