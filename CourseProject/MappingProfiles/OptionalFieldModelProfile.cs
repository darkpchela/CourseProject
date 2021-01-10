using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldModelProfile : Profile
    {
        public OptionalFieldModelProfile()
        {
            CreateMap<OptionalFieldModel, OptionalFieldVM>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Type));
            CreateMap<CreateOptionalFieldVM, OptionalFieldModel>();
        }
    }
}