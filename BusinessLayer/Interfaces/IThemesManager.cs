using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IThemesManager
    {
        Task<CreateThemeResult> CreateAsync(CreateThemeModel createThemeModel);

        Task<DeleteThemeResult> DeleteAsync(DeleteThemeModel deleteThemeModel);

        Task<UpdateThemeResult> UpdateAsync(UpdateThemeModel updateThemeModel);
    }
}