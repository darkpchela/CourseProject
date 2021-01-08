using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.MappingProfiles
{
    public class CreateCollectionProfile : Profile
    {
        public CreateCollectionProfile(IMapper mapper)
        {
            CreateMap<CreateCollectionVM, CreateCollectionModel>()
                .ForMember(d => d.Fields, o => o.MapFrom(s => mapper.Map<OptionalFieldModel>(s.Fields)));
        }
    }
}
