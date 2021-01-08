using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class OptionalFieldProfile : Profile
    {
        public OptionalFieldProfile()
        {
            CreateMap<OptionalField, OptionalFieldModel>().ReverseMap();
        }
    }
}