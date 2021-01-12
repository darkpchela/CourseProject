using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class DeleteOptionalFieldModelProfile : Profile
    {
        public DeleteOptionalFieldModelProfile()
        {
            CreateMap<DeleteOptionalFieldVM, DeleteOptionalFieldModel>();
        }
    }
}