using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ThemesManager : IThemesManager
    {
        private readonly IThemesCrudService themesCrudService;

        private readonly ICreateThemeModelValidator createThemeModelValidator;

        private readonly IUpdateThemeModelValidator updateThemeModelValidator;

        private readonly IDeleteThemeModelValidator deleteThemeModelValidator;

        private readonly IMapper mapper;

        public ThemesManager(IThemesCrudService themesCrudService, ICreateThemeModelValidator createThemeModelValidator, IUpdateThemeModelValidator updateThemeModelValidator, IDeleteThemeModelValidator deleteThemeModelValidator, IMapper mapper)
        {
            this.themesCrudService = themesCrudService;
            this.createThemeModelValidator = createThemeModelValidator;
            this.updateThemeModelValidator = updateThemeModelValidator;
            this.deleteThemeModelValidator = deleteThemeModelValidator;
            this.mapper = mapper;
        }

        public async Task<CreateThemeResult> CreateAsync(CreateThemeModel createThemeModel)
        {
            var validResult = await createThemeModelValidator.ValidateAsync(createThemeModel);
            if (!validResult.Succeed)
                return new CreateThemeResult(validResult);
            var model = mapper.Map<ThemeModel>(createThemeModel);
            await themesCrudService.CreateAsync(model);
            var result = new CreateThemeResult
            {
                CreatedThemeId = model.Id,
                CreatedThemeName = model.Name
            };
            return result;
        }

        public async Task<DeleteThemeResult> DeleteAsync(DeleteThemeModel deleteThemeModel)
        {
            var validRes = await deleteThemeModelValidator.ValidateAsync(deleteThemeModel);
            if (!validRes.Succeed)
                return new DeleteThemeResult(validRes);
            await themesCrudService.DeleteAsync(deleteThemeModel.ThemeId);
            var result = new DeleteThemeResult
            {
                DeletedThemeId = deleteThemeModel.ThemeId
            };
            return result;
        }

        public async Task<UpdateThemeResult> UpdateAsync(UpdateThemeModel updateThemeModel)
        {
            var validRes = await updateThemeModelValidator.ValidateAsync(updateThemeModel);
            if (!validRes.Succeed)
                return new UpdateThemeResult(validRes);
            var model = mapper.Map<ThemeModel>(updateThemeModel);
            await themesCrudService.UpdateAsync(model);
            var result = new UpdateThemeResult()
            {
                UpdatedThemeId = model.Id,
                Name = model.Name
            };
            return result;
        }
    }
}