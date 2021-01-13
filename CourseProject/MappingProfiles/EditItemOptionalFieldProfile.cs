using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class EditItemOptionalFieldProfile : Profile
    {
        public EditItemOptionalFieldProfile()
        {
            CreateMap<ItemOptionalFieldModel, EditItemOptionalFieldVM>()
                .ForMember(d => d.FieldId, o => o.MapFrom(s => s.OptionalFieldId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.OptionalField.Name))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.OptionalField.Type.Type));
        }
    }
}