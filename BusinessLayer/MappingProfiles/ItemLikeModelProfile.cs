using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class ItemLikeModelProfile : Profile
    {
        public ItemLikeModelProfile()
        {
            CreateMap<LikeItemModel, ItemLikeModel>();
        }
    }
}