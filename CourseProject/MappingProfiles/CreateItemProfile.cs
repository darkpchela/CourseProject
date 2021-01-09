using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.MappingProfiles
{
    public class CreateItemProfile : Profile
    {
        public CreateItemProfile()
        {
            CreateMap<CreateItemVM, CreateItemModel>();
        }
    }
}
