using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class CreateItemModelProfile : Profile
    {
        public CreateItemModelProfile()
        {
            CreateMap<CreateItemVM, CreateItemModel>()
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields));
        }
    }
}