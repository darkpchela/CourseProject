using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class CreateCollectionProfile : Profile
    {
        public CreateCollectionProfile()
        {
            CreateMap<CreateCollectionVM, CreateCollectionModel>()
                .ForMember(d => d.Fields, o => o.MapFrom(s => s.Fields));
        }
    }
}