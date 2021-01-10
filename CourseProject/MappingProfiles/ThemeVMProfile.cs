using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class ThemeVMProfile : Profile
    {
        public ThemeVMProfile()
        {
            CreateMap<ThemeModel, ThemeVM>();
        }
    }
}