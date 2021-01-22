using AutoMapper;
using BusinessLayer.Models.DALModels;
using CloudinaryDotNet.Actions;

namespace BusinessLayer.MappingProfiles
{
    public class ResourceModelProfile : Profile
    {
        public ResourceModelProfile()
        {
            CreateMap<UploadResult, ResourceModel>()
                .ForMember(d => d.PublicId, o => o.MapFrom(s => s.PublicId))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Uri));
        }
    }
}