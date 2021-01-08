using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Collections.Generic;
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

        public async Task<UploadResultModel> UploadAsync(FileDescription fileDescription)
        {
            var uploadParams = new ImageUploadParams
            {
                Folder = "CourseProject",
                File = fileDescription
            };
            var result = await cloudinary.UploadAsync(uploadParams);
            var model = new UploadResultModel
            {
                Succeed = result.Error is null ? true : false,
                Error = result.Error?.Message ?? "",
                Uri = result.Uri?.AbsoluteUri ?? "",
                ObjectId = result.PublicId
            };
            return model;
        }

        public async Task<GetResourceModel> GetUri(string pulbicId)
        {
            var getParams = new GetResourceParams(pulbicId);
            var result = await cloudinary.GetResourceAsync(getParams);
            var model = new GetResourceModel
            {
                Succeed = result.Error is null ? true : false,
                Error = result.Error?.Message ?? "",
                Uri = result.Url
            };
            return model;
        }
    }
}