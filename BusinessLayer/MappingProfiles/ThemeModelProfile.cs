using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class ThemeModelProfile : Profile
    {
        public ThemeModelProfile()
        {
            CreateMap<CreateThemeModel, ThemeModel>();
            CreateMap<UpdateThemeModel, ThemeModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ThemeId));
        }
    }
}