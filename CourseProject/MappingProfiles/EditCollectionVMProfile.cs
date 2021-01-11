using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class EditCollectionVMProfile : Profile
    {
        public EditCollectionVMProfile()
        {
            CreateMap<CollectionModel, EditCollectionVM>();
        }
    }
}