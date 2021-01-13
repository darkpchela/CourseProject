using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using System.Linq;

namespace CourseProject.MappingProfiles
{
    public class EditItemVMProfile : Profile
    {
        public EditItemVMProfile()
        {
            CreateMap<ItemModel, EditItemVM>()
                .ForMember(d => d.ItemId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.CollectionId, o => o.MapFrom(s => s.CollectionItems.FirstOrDefault().CollectionId))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Resource.Url))
                .ForMember(d => d.Fields, o => o.MapFrom(s => s.ItemOptionalFields));
        }
    }
}