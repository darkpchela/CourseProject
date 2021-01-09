using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldProfile : Profile
    {
        public OptionalFieldProfile()
        {
            CreateMap<OptionalFieldModel, OptionalFieldVM>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Type));
            CreateMap<CreateOptionalFieldVM, OptionalFieldModel>();
        }
    }
}