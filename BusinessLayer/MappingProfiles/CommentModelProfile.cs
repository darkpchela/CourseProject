﻿using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;

namespace BusinessLayer.MappingProfiles
{
    public class CommentModelProfile : Profile
    {
        public CommentModelProfile()
        {
            CreateMap<CommentItemModel, CommentModel>();
        }
    }
}