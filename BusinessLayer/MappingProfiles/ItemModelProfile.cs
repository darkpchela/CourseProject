using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class ItemModelProfile : Profile
    {
        public ItemModelProfile()
        {
            CreateMap<CreateItemModel, ItemModel>()
                .ForMember(d => d.ItemOptionalFields, o => o.MapFrom(s => s.OptionalFields));
            CreateMap<UpdateItemModel, ItemModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ItemId))
                .ForMember(d => d.ItemOptionalFields, o => o.MapFrom(s => s.OptionalFields));
        }
    }
}