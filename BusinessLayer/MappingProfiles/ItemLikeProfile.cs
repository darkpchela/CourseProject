using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class ItemLikeProfile : Profile
    {
        public ItemLikeProfile()
        {
            CreateMap<ItemLike, ItemLikeModel>().ReverseMap();
        }
    }
}