using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
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

        public Task DeleteAsync(string url)
        {
            throw new NotImplementedException();
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
                Uri = result.Uri?.AbsoluteUri ?? ""
            };
            return model;
        }
    }
}