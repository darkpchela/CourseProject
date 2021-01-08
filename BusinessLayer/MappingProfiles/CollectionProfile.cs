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
            CreateMap<CreateCollectionModel, Collection>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImagePublicKey))
                .ForMember(d => d.CreationDate, o => o.MapFrom(s => s.CreatedAt));
        }
    }
}