using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class ItemCommentProfile : Profile
    {
        public ItemCommentProfile()
        {
            CreateMap<ItemComment, ItemCommentModel>().ReverseMap();
        }
    }
}