using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class CollectionOptionalFieldProfile : Profile
    {
        public CollectionOptionalFieldProfile()
        {
            CreateMap<CollectionOptionalField, CollectionOptionalFieldModel>().ReverseMap();
        }
    }
}