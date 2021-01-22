using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BusinessLayer.MappingProfiles
{
    public class UserModelProfile : Profile
    {
        public UserModelProfile()
        {
            CreateMap<ExternalLoginInfo, UserModel>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.GivenName)))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.Surname)))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Principal.FindFirstValue(ClaimTypes.Email)));
            CreateMap<AppUser, UserModel>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Email));
            CreateMap<SignUpModel, UserModel>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Email));
        }
    }
}