using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class TagJsonVMProfile : Profile
    {
        public TagJsonVMProfile()
        {
            CreateMap<TagModel, TagJsonVM>()
                .ForMember(d => d.value, o => o.MapFrom(s => s.Value));
        }
    }
}