using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using System.Linq;

namespace CourseProject.MappingProfiles
{
    public class ItemVMProfile : Profile
    {
        public ItemVMProfile()
        {
            CreateMap<ItemModel, ItemVM>()
                .ForMember(d => d.Fields, o => o.MapFrom(s => s.ItemOptionalFields))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Resource.Url))
                .ForMember(d => d.Collections, o => o.MapFrom(s => s.CollectionItems.Select(ci => ci.Collection)));
        }
    }
}