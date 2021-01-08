using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentModel>().ReverseMap();
        }
    }
}