using AutoMapper;
using BusinessLayer.Models;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.MappingProfiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<ExternalLoginInfo, AppUser>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.Email)))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.Email)));
            CreateMap<SignUpModel, AppUser>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));
        }
    }
}