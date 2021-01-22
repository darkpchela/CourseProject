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

        private readonly ICommentsCrudService commentsCrudService;

        private readonly IMapper mapper;

        public CommentsManager(IModelValidatorsStore validatorsStore, IItemCommentCrudService itemCommentCrudService, ICommentsCrudService commentsCrudService ,IMapper mapper)
        {
            this.validatorsStore = validatorsStore;
            this.itemCommentCrudService = itemCommentCrudService;
            this.commentsCrudService = commentsCrudService;
            this.mapper = mapper;
        }

        public async Task<CommentItemResult> CommentItemAsync(CommentItemModel commentItemModel)
        {
            var validRes = await validatorsStore.CommentItemModelValidator.ValidateAsync(commentItemModel);
            if (!validRes.Succeed)
                return new CommentItemResult(validRes);
            var comment = mapper.Map<CommentModel>(commentItemModel);
            comment.CreationDate = DateTime.Now;
            await commentsCrudService.CreateAsync(comment);
            var itemComment = new ItemCommentModel
            {
                CommentId = comment.Id,
                ItemId = commentItemModel.ItemId
            };
            await itemCommentCrudService.CreateAsync(itemComment);
            var result = mapper.Map<CommentItemResult>(comment);
            return result;
        }
    }
}