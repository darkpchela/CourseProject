using AutoMapper;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class TagModelProfile : Profile
    {
        public TagModelProfile()
        {
            CreateMap<string, TagModel>()
                .ForMember(d => d.Value, o => o.MapFrom(s => s));
        }
    }
}