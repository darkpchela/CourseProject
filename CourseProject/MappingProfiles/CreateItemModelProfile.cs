using AutoMapper;
using BusinessLayer.Extensions;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using System.Linq;

namespace CourseProject.MappingProfiles
{
    public class CreateItemModelProfile : Profile
    {
        public CreateItemModelProfile()
        {
            CreateMap<CreateItemVM, CreateItemModel>()
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields))
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.TagsJson.JsonDeserialize<TagJsonVM[]>().Select(t => t.value)))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Trim()));
        }
    }
}