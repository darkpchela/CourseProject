using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldModelProfile : Profile
    {
        public OptionalFieldModelProfile()
        {
            CreateMap<CreateOptionalFieldVM, OptionalFieldModel>();
            CreateMap<EditOptionalFieldVM, OptionalFieldModel>();
        }
    }
}