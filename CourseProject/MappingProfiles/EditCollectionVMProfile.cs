using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class EditCollectionVMProfile : Profile
    {
        public EditCollectionVMProfile()
        {
            CreateMap<CollectionModel, EditCollectionVM>()
                .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.Resource.Url));
        }
    }
}