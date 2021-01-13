using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldModelProfile : Profile
    {
        public OptionalFieldModelProfile()
        {
            CreateMap<CreateCollectionOptionalFieldVM, OptionalFieldModel>();
            CreateMap<EditOptionalFieldVM, OptionalFieldModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.FieldId));
        }
    }
}