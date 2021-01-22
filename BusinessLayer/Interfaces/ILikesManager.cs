using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILikesManager
    {
        Task<LikeItemResult> LikeItemAsync(LikeItemModel likeItemModel);
    }
}