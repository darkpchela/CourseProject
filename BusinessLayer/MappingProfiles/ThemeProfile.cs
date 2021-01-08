using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class ThemeProfile : Profile
    {
        public ThemeProfile()
        {
            CreateMap<Theme, ThemeModel>().ReverseMap();
        }
    }
}