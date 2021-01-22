using BusinessLayer.Models;
using BusinessLayer.Models.ResultModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICommentsManager
    {
        Task<CommentItemResult> CommentItemAsync(CommentItemModel commentItemModel);
    }
}
