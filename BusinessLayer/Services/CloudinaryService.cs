using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task DeleteAsync(string publicId)
        {
            var delParams = new DelResParams
            {
                ResourceType = ResourceType.Image,
                PublicIds = new List<string>
                {
                    publicId
                }
            };
            await cloudinary.DeleteResourcesAsync(delParams);
        }

        public async Task<UploadResult> UploadAsync(string name, Stream fileStream)
        {
            var fileDesc = new FileDescription(name, fileStream);
            var uploadParams = new ImageUploadParams
            {
                Folder = "CourseProject",
                File = fileDesc
            };
            var result = await cloudinary.UploadAsync(uploadParams);
            return result;
        }

        public async Task<GetResourceResult> GetUri(string pulbicId)
        {
            var getParams = new GetResourceParams(pulbicId);
            var result = await cloudinary.GetResourceAsync(getParams);
            return result;
        }
    }
}