using AutoMapper;
using BusinessLayer.Extensions;
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
            CreateMap<ExternalLoginInfo, UserModel>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.GivenName)))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.Surname)))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.Email).UntilChar('@')));
            CreateMap<AppUser, UserModel>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Email.UntilChar('@')));
            CreateMap<SignUpModel, UserModel>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Email.UntilChar('@')));
        }
    }
}