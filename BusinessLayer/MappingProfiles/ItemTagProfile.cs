using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class ItemTagProfile : Profile
    {
        public ItemTagProfile()
        {
            CreateMap<ItemTag, ItemTagModel>().ReverseMap();
        }
    }
}