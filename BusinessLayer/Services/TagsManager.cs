using AutoMapper;
using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.BaseCrud;
using BusinessLayer.Models.DALModels;
using BusinessLayer.Models.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TagsManager : ITagsManager
    {
        private readonly ITagsCrudService tagsCrudService;

        private readonly IItemTagCrudService itemTagCrudService;

        private readonly IMapper mapper;

        public TagsManager(ITagsCrudService tagsCrudService, IItemTagCrudService itemTagCrudService, IMapper mapper)
        {
            this.tagsCrudService = tagsCrudService;
            this.itemTagCrudService = itemTagCrudService;
            this.mapper = mapper;
        }

        public async Task AttachTagsToItemAsync(IEnumerable<TagModel> tags, int itemId)
        {
            var itemTags = new List<ItemTagModel>();
            foreach (var tag in tags)
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

        public async Task<FindTagsResult> FindTagsAsync(IEnumerable<string> values)
        {
            var result = new FindTagsResult();
            var allTags = await tagsCrudService.GetAllAsync();
            var tags = allTags.Where(t => values.Contains(t.Value));
            result.Founded = tags;
            return result;
        }

        public async Task<CreateTagsResult> CreateTagsAsync(IEnumerable<string> values)
        {
            var result = new CreateTagsResult();
            var allTags = await tagsCrudService.GetAllAsync();
            var newValues = values.Except(allTags.Select(t => t.Value)).ToList();
            var newTags = mapper.Map<IEnumerable<TagModel>>(newValues);
            await tagsCrudService.CreateRangeAsync(newTags);
            result.Created = newTags;
            return result;
        }
    }
}