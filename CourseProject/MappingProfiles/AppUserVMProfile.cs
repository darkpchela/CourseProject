using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using Identity.Entities;

namespace CourseProject.MappingProfiles
{
    public class AppUserVMProfile : Profile
    {
        public AppUserVMProfile()
        {
            CreateMap<AppUser, AppUserVM>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName));
            CreateMap<UserModel, AppUserVM>();
        }
    }
}