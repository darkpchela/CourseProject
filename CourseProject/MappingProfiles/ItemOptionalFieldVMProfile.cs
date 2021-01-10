using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class ItemOptionalFieldVMProfile : Profile
    {
        public ItemOptionalFieldVMProfile()
        {
            CreateMap<ItemOptionalFieldModel, ItemOptionalFieldVM>()
                .ForMember(d => d.Type, o => o.MapFrom(s => s.OptionalField.Type.Type))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.OptionalField.Name));
        }
    }
}