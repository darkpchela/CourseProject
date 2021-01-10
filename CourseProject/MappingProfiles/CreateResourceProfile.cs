using AutoMapper;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;

namespace CourseProject.MappingProfiles
{
    public class CreateResourceProfile : Profile
    {
        public CreateResourceProfile()
        {
            CreateMap<IFormFile, CreateResourceModel>()
                .ForMember(d => d.FileStream, o => o.MapFrom(s => s.OpenReadStream()))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FileName));
        }
    }
}