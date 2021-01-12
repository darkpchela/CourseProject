using AutoMapper;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;

namespace BusinessLayer.MappingProfiles
{
    public class CreateResourceResultProfile : Profile
    {
        public CreateResourceResultProfile()
        {
            CreateMap<ResourceModel, CreateResourceResult>();
        }
    }
}