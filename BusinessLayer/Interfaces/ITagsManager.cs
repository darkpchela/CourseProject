using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ITagsManager
    {
        Task AttachTagsToItemAsync(IEnumerable<TagModel> tags, int itemId);

        Task<CreateTagsResult> CreateTagsAsync(IEnumerable<string> values);

        Task<FindTagsResult> FindTagsAsync(IEnumerable<string> values);
    }
}