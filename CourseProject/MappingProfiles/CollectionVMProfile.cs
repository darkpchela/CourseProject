using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using System.Linq;

namespace CourseProject.MappingProfiles
{
    public class CollectionVMProfile : Profile
    {
        public CollectionVMProfile()
        {
            CreateMap<CollectionModel, CollectionVM>()
                .ForMember(d => d.Items, o => o.MapFrom(s => s.CollectionItems.Select(ci => ci.Item)))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Resource.Url))
                .ForMember(d => d.Fields, o => o.MapFrom(s => s.OptionalFields));
        }
    }
}