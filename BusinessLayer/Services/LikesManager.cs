using AutoMapper;
using BusinessLayer.Etc;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LikesManager : ILikesManager
    {
        private readonly IItemLikeCrudService itemLikeCrudService;

        private readonly IModelValidatorsStore modelValidatorsStore;

        private readonly IMapper mapper;

        public LikesManager(IItemLikeCrudService itemLikeCrudService, IModelValidatorsStore modelValidatorsStore, IMapper mapper)
        {
            this.itemLikeCrudService = itemLikeCrudService;
            this.modelValidatorsStore = modelValidatorsStore;
            this.mapper = mapper;
        }

        public async Task<LikeItemResult> LikeItemAsync(LikeItemModel likeItemModel)
        {
            var validRes = await modelValidatorsStore.LikeItemModelValidator.ValidateAsync(likeItemModel);
            if (!validRes.Succeed)
                return new LikeItemResult(validRes);
            var itemLike = (await itemLikeCrudService.GetAllAsync()).FirstOrDefault(il => il.ItemId == likeItemModel.ItemId && il.UserId == likeItemModel.UserId);
            var result = new LikeItemResult();
            if (itemLike is null)
            {
                var newLike = mapper.Map<ItemLikeModel>(likeItemModel);
                await itemLikeCrudService.CreateAsync(newLike);
                result.Result = LikeResults.Liked;
            }
            else
            {
                await itemLikeCrudService.DeleteAsync(itemLike.Id);
                result.Result = LikeResults.Disliked;
            }
            return result;
        }
    }
}