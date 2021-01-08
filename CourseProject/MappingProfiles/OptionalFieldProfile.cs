using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldProfile : Profile
    {
        public OptionalFieldProfile()
        {
            CreateMap<OptionalFieldVM, OptionalFieldModel>();
        }
    }
}