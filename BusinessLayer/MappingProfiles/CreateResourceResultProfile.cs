using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class CreateResourceResultProfile : Profile
    {
        public CreateResourceResultProfile()
        {
            CreateMap<ResourceModel, CreateResourceResultModel>();
        }
    }
}