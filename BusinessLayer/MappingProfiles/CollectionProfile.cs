using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<Collection, CollectionModel>().ReverseMap();
            CreateMap<CreateCollectionModel, CollectionModel>();
        }
    }
}