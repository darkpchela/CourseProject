using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class ItemModelProfile : Profile
    {
        public ItemModelProfile()
        {
            CreateMap<CreateItemVM, ItemModel>()
                    .ForMember(d => d.ItemOptionalFields, o => o.MapFrom(s => s.Fields));
        }
    }
}