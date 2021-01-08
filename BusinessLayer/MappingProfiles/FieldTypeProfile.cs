using AutoMapper;
using BusinessLayer.Models.DALModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.MappingProfiles
{
    public class FieldTypeProfile : Profile
    {
        public FieldTypeProfile()
        {
            CreateMap<FieldType, FieldTypeModel>().ReverseMap();
        }
    }
}
