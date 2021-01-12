using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels.ApiModels;

namespace CourseProject.MappingProfiles
{
    public class CreateDefaultOptionalFieldModelProfile : Profile
    {
        public CreateDefaultOptionalFieldModelProfile()
        {
            CreateMap<CreateDefaultOptionalFieldVM, CreateDefaultOptionalFieldModel>();
        }
    }
}