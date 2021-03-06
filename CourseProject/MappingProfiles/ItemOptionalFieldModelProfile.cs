﻿using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using CourseProject.ViewModels.EditableModels;

namespace CourseProject.MappingProfiles
{
    public class ItemOptionalFieldModelProfile : Profile
    {
        public ItemOptionalFieldModelProfile()
        {
            CreateMap<CreateItemOptionalFieldVM, ItemOptionalFieldModel>()
                .ForMember(d => d.OptionalFieldId, o => o.MapFrom(s => s.FieldId));
            CreateMap<EditItemOptionalFieldVM, ItemOptionalFieldModel>()
                .ForMember(d => d.OptionalFieldId, o => o.MapFrom(s => s.FieldId))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ItemFieldId));
        }
    }
}