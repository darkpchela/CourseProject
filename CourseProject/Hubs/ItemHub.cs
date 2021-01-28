using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CourseProject.Hubs
{
    public class ItemHub : Hub
    {
        private readonly ILikesManager likesManager;

        private readonly ICommentsManager commentsManager;

        private readonly ISessionHelper sessionHelper;

        public ItemHub(ILikesManager likesManager, ICommentsManager commentsManager, ISessionHelper sessionHelper) : base()
        {
            this.likesManager = likesManager;
            this.commentsManager = commentsManager;
            this.sessionHelper = sessionHelper;
        }

        public async override Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("OnConnected", Context.ConnectionId);
        }

        public async Task ConnectGroup(string itemId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, itemId.ToString());
        }

        public async Task SendLike(string itemId)
        {
            if (!int.TryParse(itemId, out int parsedId))
                return;
            var model = new LikeItemModel
            {
                UserId = sessionHelper.GetCurrentUserId(),
                ItemId = parsedId
            };
            var result = await likesManager.LikeItemAsync(model);
            await Clients.Group(itemId.ToString()).SendAsync("OnItemLiked", result, Context.ConnectionId);
        }

        public async Task SendComment(string itemId, string value)
        {
            Thread.Sleep(3000);
            if (!int.TryParse(itemId, out int parsedId))
                return;
            var model = new CommentItemModel
            {
                ItemId = parsedId,
                UserId = sessionHelper.GetCurrentUserId(),
                Value = value
            };
            var result = await commentsManager.CommentItemAsync(model);
            await Clients.Group(itemId.ToString()).SendAsync("OnCommentMade", result, Context.ConnectionId);
        }
    }
}