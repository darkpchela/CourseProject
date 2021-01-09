using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.MappingProfiles
{
    public class ItemOptionalFieldProfile : Profile
    {
        public ItemOptionalFieldProfile()
        {
            CreateMap<EditItemOptionalFieldVM, ItemOptionalFieldModel>();
        }
    }
}
