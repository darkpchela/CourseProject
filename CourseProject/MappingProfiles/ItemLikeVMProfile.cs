using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class ItemLikeVMProfile : Profile
    {
        public ItemLikeVMProfile()
        {
            CreateMap<ItemLikeModel, ItemLikeVM>();
        }
    }
}