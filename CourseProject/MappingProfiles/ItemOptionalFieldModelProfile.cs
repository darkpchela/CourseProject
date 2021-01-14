using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class ItemOptionalFieldModelProfile : Profile
    {
        public ItemOptionalFieldModelProfile()
        {
            CreateMap<EditItemOptionalFieldVM, ItemOptionalFieldModel>()
                .ForMember(d => d.OptionalFieldId, o => o.MapFrom(s => s.FieldId))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ItemFieldId));
        }
    }
}