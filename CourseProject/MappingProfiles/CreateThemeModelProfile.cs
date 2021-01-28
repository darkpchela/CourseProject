using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels.EditableModels;

namespace CourseProject.MappingProfiles
{
    public class CreateThemeModelProfile : Profile
    {
        public CreateThemeModelProfile()
        {
            CreateMap<CreateThemeVM, CreateThemeModel>();
        }
    }
}