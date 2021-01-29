using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using System;

namespace BusinessLayer.MappingProfiles
{
    public class ItemModelProfile : Profile
    {
        public ItemModelProfile()
        {
            CreateMap<CreateItemModel, ItemModel>()
                .ForMember(d => d.ItemOptionalFields, o => o.MapFrom(s => s.OptionalFields))
                .ForMember(d => d.CreationDate, o => o.MapFrom(s => DateTime.Now));
            CreateMap<UpdateItemModel, ItemModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ItemId))
                .ForMember(d => d.ItemOptionalFields, o => o.MapFrom(s => s.OptionalFields));
        }
    }
}