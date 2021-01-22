using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class ItemCommentModelProfile : Profile
    {
        public ItemCommentModelProfile()
        {
            CreateMap<CommentItemModel, ItemCommentModel>()
                .ForMember(d => d.ItemId, o => o.MapFrom(s => s.ItemId))
                .ForMember(d => d.Comment.CreationDate, o => o.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.Comment.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(d => d.Comment.Value, o => o.MapFrom(s => s.Value));
        }
    }
}