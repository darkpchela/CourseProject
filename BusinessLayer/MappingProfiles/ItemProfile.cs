using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemModel>().ReverseMap();
            CreateMap<CreateItemModel, ItemModel>()
                .ForMember(d => d.ItemOptionalFields, o => o.MapFrom(s => s.Fields))
                .ForMember(d => d.OwnerId, o => o.MapFrom(s => s.CreatorId))
                .ForMember(d => d.CreationDate, o=> o.MapFrom(s => s.CreatedAt));
        }
    }
}