using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class CollectionModelProfile : Profile
    {
        public CollectionModelProfile()
        {
            CreateMap<CreateCollectionModel, CollectionModel>();
            CreateMap<UpdateCollectionModel, CollectionModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CollectionId));
        }
    }
}