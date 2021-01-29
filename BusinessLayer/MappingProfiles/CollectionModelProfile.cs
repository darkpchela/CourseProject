using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System;

namespace BusinessLayer.MappingProfiles
{
    public class CollectionModelProfile : Profile
    {
        public CollectionModelProfile()
        {
            CreateMap<CreateCollectionModel, CollectionModel>()
                .ForMember(d => d.CreationDate, o => o.MapFrom(s => DateTime.Now));
            CreateMap<UpdateCollectionModel, CollectionModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CollectionId));
        }
    }
}