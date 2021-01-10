using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class OptionalFieldVMProfile : Profile
    {
        public OptionalFieldVMProfile()
        {
            CreateMap<OptionalFieldModel, OptionalFieldVM>()
              .ForMember(d => d.Type, o => o.MapFrom(s => s.Type.Type));
        }
    }
}