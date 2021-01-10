using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;
using UploadResult = CloudinaryDotNet.Actions.UploadResult;

namespace BusinessLayer.MappingProfiles
{
    public class ResourceProfile : Profile
    {
        public ResourceProfile()
        {
            CreateMap<Resource, ResourceModel>().ReverseMap();
            CreateMap<UploadResult, ResourceModel>()
                .ForMember(d => d.PublicId, o => o.MapFrom(s => s.PublicId))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Uri));
        }
    }
}