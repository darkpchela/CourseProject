﻿using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using System.Linq;

namespace CourseProject.MappingProfiles
{
    public class CollectionVMProfile : Profile
    {
        public CollectionVMProfile()
        {
            CreateMap<CollectionModel, CollectionVM>()
                .ForMember(d => d.Items, o => o.MapFrom(s => s.CollectionItems.Select(ci => ci.Item)));
        }
    }
}