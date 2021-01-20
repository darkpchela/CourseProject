using AutoMapper;
using BusinessLayer.Extensions;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using System.Linq;

namespace CourseProject.MappingProfiles
{
    public class UpdateItemModelProfile : Profile
    {
        public UpdateItemModelProfile()
        {
            CreateMap<EditItemVM, UpdateItemModel>()
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields))
                .ForMember(d => d.Tags, o => o.MapFrom(s => s.TagsJson.JsonDeserialize<TagJsonVM[]>().Select(t => t.value)));
        }
    }
}