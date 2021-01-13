using AutoMapper;
using BusinessLayer.Models.DALModels;
using CourseProject.ViewModels;

namespace CourseProject.MappingProfiles
{
    public class EditOptionalFiedlVMProfile : Profile
    {
        public EditOptionalFiedlVMProfile()
        {
            CreateMap<OptionalFieldModel, EditOptionalFieldVM>()
                .ForMember(d => d.FieldId, o => o.MapFrom(s => s.Id));
        }
    }
}