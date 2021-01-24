using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TagsManager : ITagsManager
    {
        private readonly ITagsCrudService tagsCrudService;

        private readonly IItemTagCrudService itemTagCrudService;

        private readonly IItemsCrudService itemsCrudService;

        private readonly IMapper mapper;

        public TagsManager(ITagsCrudService tagsCrudService, IItemTagCrudService itemTagCrudService, IItemsCrudService itemsCrudService, IMapper mapper)
        {
            this.tagsCrudService = tagsCrudService;
            this.itemTagCrudService = itemTagCrudService;
            this.itemsCrudService = itemsCrudService;
            this.mapper = mapper;
        }

        public async Task AttachTagsToItemAsync(IEnumerable<string> values, int itemId)
        {
            await ClearItemTags(itemId);
            var itemTags = new List<ItemTagModel>();
            var newTags = await MaterializeTagsByValues(values);
            foreach (var tag in newTags)
            {
                var itemTag = new ItemTagModel
                {
                    ItemId = itemId,
                    TagId = tag.Id
                };
                itemTags.Add(itemTag);
            }
            await itemTagCrudService.CreateRangeAsync(itemTags);
        }

        private async Task<IEnumerable<TagModel>> MaterializeTagsByValues(IEnumerable<string> values)
        {
            var tags = new List<TagModel>();
            var foundRes = await FindTagsAsync(values);
            tags.AddRange(foundRes.Founded);
            if (foundRes.Founded.Count() < values.Count())
            {
                var createRes = await CreateTagsAsync(values);
                tags.AddRange(createRes.Created);
            }
            return tags;
        }

        private async Task<FindTagsResult> FindTagsAsync(IEnumerable<string> values)
        {
            var result = new FindTagsResult();
            var allTags = await tagsCrudService.GetAllAsync();
            var tags = allTags.Where(t => values.Contains(t.Value));
            result.Founded = tags;
            return result;
        }

        private async Task<CreateTagsResult> CreateTagsAsync(IEnumerable<string> values)
        {
            var result = new CreateTagsResult();
            var allTags = await tagsCrudService.GetAllAsync();
            var newValues = values.Except(allTags.Select(t => t.Value)).ToList();
            var newTags = mapper.Map<IEnumerable<TagModel>>(newValues);
            await tagsCrudService.CreateRangeAsync(newTags);
            result.Created = newTags;
            return result;
        }

        private async Task ClearItemTags(int itemId)
        {
            var item = await itemsCrudService.GetAsync(itemId);
            var itemTags = item.ItemTags.ToList();
            await itemTagCrudService.DeleteRangeAsync(itemTags.Select(it => it.Id));
        }
    }
}