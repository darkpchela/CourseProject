using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class CollectionItemProfile : Profile
    {
        public CollectionItemProfile()
        {
            CreateMap<CollectionItem, CollectionItemModel>().ReverseMap();
        }
    }
}