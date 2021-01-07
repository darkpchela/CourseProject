using BusinessLayer.Models;
using CloudinaryDotNet.Actions;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICloudinaryService
    {
        Task<UploadResultModel> UploadAsync(FileDescription fileDescription);

        Task DeleteAsync(string url);
    }
}