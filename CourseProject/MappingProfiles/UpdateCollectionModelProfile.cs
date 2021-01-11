using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class UpdateCollectionModelProfile : Profile
    {
        public UpdateCollectionModelProfile()
        {
            CreateMap<EditCollectionVM, UpdateCollectionModel>()
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields));
        }
    }
}