using AutoMapper;
using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class ItemOptionalFieldProfile : Profile
    {
        public ItemOptionalFieldProfile()
        {
            CreateMap<ItemOptionalField, ItemOptionalFieldModel>().ReverseMap();
        }
    }
}