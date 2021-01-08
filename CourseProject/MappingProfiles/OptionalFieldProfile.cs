using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldProfile : Profile
    {
        public OptionalFieldProfile()
        {
            CreateMap<OptionalFieldModel, CollectionOptionalFieldVM>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Type));
            CreateMap<CollectionOptionalFieldModel, CollectionOptionalFieldVM>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.OptionalField.Name))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.OptionalField.Type.Type));
            CreateMap<CreateOptionalFieldVM, OptionalFieldModel>();
        }
    }
}