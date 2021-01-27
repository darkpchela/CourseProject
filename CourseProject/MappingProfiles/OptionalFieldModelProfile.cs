using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldModelProfile : Profile
    {
        public OptionalFieldModelProfile()
        {
            CreateMap<CreateCollectionOptionalFieldVM, OptionalFieldModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()));
            CreateMap<EditOptionalFieldVM, OptionalFieldModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.FieldId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()));
        }
    }
}