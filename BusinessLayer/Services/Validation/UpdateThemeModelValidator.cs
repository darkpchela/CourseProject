using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class UpdateThemeModelValidator : DefaultValidator<UpdateThemeModel>, IUpdateThemeModelValidator
    {
        private readonly IThemesCrudService themesCrudService;

        public UpdateThemeModelValidator(IThemesCrudService themesCrudService)
        {
            this.themesCrudService = themesCrudService;
        }

        protected async override Task BaseValidation(UpdateThemeModel model)
        {
            var allThemes = await themesCrudService.GetAllAsync();
            if (!allThemes.Any(t => t.Id == model.ThemeId))
                ValidationResult.AddError("Theme not found");
            if (allThemes.Any(t => t.Name == model.Name))
                ValidationResult.AddError("Theme with such name already exists");
        }

        protected async override Task OptionalValidation(UpdateThemeModel model)
        {
            return;
        }
    }
}