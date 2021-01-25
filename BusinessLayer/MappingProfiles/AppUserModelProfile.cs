using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class AppUserModelProfile : Profile
    {
        public AppUserModelProfile()
        {
            CreateMap<UserModel, AppUserModel>();
        }
    }
}