using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Validation
{
    public class CreateThemeModelValidator : DefaultValidator<CreateThemeModel>, ICreateThemeModelValidator
    {
        private readonly IThemesCrudService themesCrudService;

        public CreateThemeModelValidator(IThemesCrudService themesCrudService)
        {
            this.themesCrudService = themesCrudService;
        }

        protected async override Task BaseValidation(CreateThemeModel model)
        {
            var theme = (await themesCrudService.GetAllAsync()).FirstOrDefault(t => t.Name == model.Name);
            if (theme != null)
                ValidationResult.AddError("Theme with such name already exists");
        }

        protected async override Task OptionalValidation(CreateThemeModel model)
        {
            return;
        }
    }
}