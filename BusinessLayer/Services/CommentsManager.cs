using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Interfaces.Validation;
using BusinessLayer.Models;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CommentsManager : ICommentsManager
    {
        private readonly IModelValidatorsStore validatorsStore;

        private readonly IItemCommentCrudService itemCommentCrudService;

        private readonly IMapper mapper;

        public CommentsManager(IModelValidatorsStore validatorsStore, IItemCommentCrudService itemCommentCrudService, IMapper mapper)
        {
            this.validatorsStore = validatorsStore;
            this.itemCommentCrudService = itemCommentCrudService;
            this.mapper = mapper;
        }

        public async Task<CommentItemResult> CommentItemAsync(CommentItemModel commentItemModel)
        {
            var validRes = await validatorsStore.CommentItemModelValidator.ValidateAsync(commentItemModel);
            if (!validRes.Succeed)
                return new CommentItemResult(validRes);
            var itemComment = mapper.Map<ItemCommentModel>(commentItemModel);
            itemComment.Comment.CreationDate = DateTime.Now;
            await itemCommentCrudService.CreateAsync(itemComment);
            return new CommentItemResult();
        }
    }
}