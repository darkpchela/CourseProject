using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels.ApiModels;

namespace CourseProject.MappingProfiles
{
    public class CreateOptionalFieldModelProfile : Profile
    {
        public CreateOptionalFieldModelProfile()
        {
            CreateMap<CreateDefaultOptionalFieldVM, CreateOptionalFieldModel>();
        }
    }
}