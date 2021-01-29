using AutoMapper;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;

namespace BusinessLayer.MappingProfiles
{
    public class CommentItemResultProfile : Profile
    {
        public CommentItemResultProfile()
        {
            CreateMap<CommentModel, CommentItemResult>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.Username))
                .ForMember(d => d.Value, o => o.MapFrom(s => s.Value))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.CreationDate.ToString("g")));
        }
    }
}