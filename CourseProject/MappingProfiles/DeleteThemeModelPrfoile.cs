using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels.EditableModels;

namespace CourseProject.MappingProfiles
{
    public class DeleteThemeModelPrfoile : Profile
    {
        public DeleteThemeModelPrfoile()
        {
            CreateMap<DeleteThemeVM, DeleteThemeModel>();
        }
    }
}