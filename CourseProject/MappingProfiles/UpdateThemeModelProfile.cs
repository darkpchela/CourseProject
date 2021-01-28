using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels.EditableModels;

namespace CourseProject.MappingProfiles
{
    public class UpdateThemeModelProfile : Profile
    {
        public UpdateThemeModelProfile()
        {
            CreateMap<UpdateThemeVM, UpdateThemeModel>();
        }
    }
}