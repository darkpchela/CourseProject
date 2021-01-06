using AutoMapper;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<ExternalLoginInfo, User>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.GivenName)))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.Surname)));
            CreateMap<AppUser, User>();
            CreateMap<SignUpModel, User>();
        }
    }
}