using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using System.Linq;

namespace CourseProject.MappingProfiles
{
    public class AppUserVMProfile : Profile
    {
        public AppUserVMProfile()
        {
            CreateMap<AppUserModel, AppUserVM>()
                .ForMember(d => d.IsAdmin, o => o.MapFrom(s => s.Roles.Contains("admin")))
                .ForMember(d => d.IsBlocked, o => o.MapFrom(s => s.Roles.Contains("blocked")));
        }
    }
}