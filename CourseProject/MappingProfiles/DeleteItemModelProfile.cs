using AutoMapper;
using BusinessLayer.Models;
using CourseProject.ViewModels.ApiModels;

namespace CourseProject.MappingProfiles
{
    public class DeleteItemModelProfile : Profile
    {
        public DeleteItemModelProfile()
        {
            CreateMap<DeleteItemVM, DeleteItemModel>();
        }
    }
}