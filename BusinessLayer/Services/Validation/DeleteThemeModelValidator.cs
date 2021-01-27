using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class DeleteThemeModelValidator : DefaultValidator<DeleteThemeModel>, IDeleteThemeModelValidator
    {
        private readonly IThemesCrudService themesCrudService;

        public DeleteThemeModelValidator(IThemesCrudService themesCrudService)
        {
            this.themesCrudService = themesCrudService;
        }

        protected async override Task BaseValidation(DeleteThemeModel model)
        {
            var theme = await themesCrudService.GetAsync(model.ThemeId);
            if (theme is null)
                ValidationResult.AddError("Theme not found");
        }

        protected async override Task OptionalValidation(DeleteThemeModel model)
        {
            return;
        }
    }
}