using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class CreateCollectionModelProfile : Profile
    {
        public CreateCollectionModelProfile()
        {
            CreateMap<CreateCollectionVM, CreateCollectionModel>()
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields));
        }
    }
}