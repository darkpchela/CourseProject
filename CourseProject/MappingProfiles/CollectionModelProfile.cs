using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class CollectionModelProfile : Profile
    {
        public CollectionModelProfile()
        {
            CreateMap<CreateCollectionVM, CollectionModel>()
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()));
        }
    }
}