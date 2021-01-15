using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels.ApiModels;

namespace CourseProject.MappingProfiles
{
    public class DeleteCollectionModelProfile : Profile
    {
        public DeleteCollectionModelProfile()
        {
            CreateMap<DeleteCollectionVM, DeleteCollectionModel>();
        }
    }
}