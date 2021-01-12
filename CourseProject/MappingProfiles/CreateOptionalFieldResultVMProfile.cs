using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class CreateOptionalFieldResultVMProfile : Profile
    {
        public CreateOptionalFieldResultVMProfile()
        {
            CreateMap<CreateOptionalFieldResultModel, CreateOptionalFieldResultVM>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.OptionalFieldModel.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.OptionalFieldModel.Name))
                .ForMember(d => d.TypeId, o => o.MapFrom(s => s.OptionalFieldModel.TypeId));
        }
    }
}