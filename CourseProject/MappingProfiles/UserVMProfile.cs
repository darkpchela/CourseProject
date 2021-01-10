using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class UserVMProfile : Profile
    {
        public UserVMProfile()
        {
            CreateMap<UserModel, UserVM>().ReverseMap();
        }
    }
}