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
            CreateMap<CreateCollectionModel, CollectionModel>()
                .ForMember(d => d.CreatorId, o => o.MapFrom(s => s.OwnerId))
                .ForMember(d => d.OptionalFields, o => o.MapFrom(s => s.Fields))
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImagePublicKey))
                .ForMember(d => d.CreationDate, o => o.MapFrom(s => s.CreatedAt));
        }
    }
}