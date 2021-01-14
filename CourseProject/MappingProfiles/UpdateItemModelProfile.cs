using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class UpdateItemModelProfile : Profile
    {
        public UpdateItemModelProfile()
        {
            CreateMap<EditItemVM, UpdateItemModel>()
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields));
        }
    }
}