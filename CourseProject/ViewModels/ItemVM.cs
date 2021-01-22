using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IList<ItemOptionalFieldVM> Fields { get; set; }

        public IList<CollectionVM> Collections { get; set; }

        public IList<ItemLikeVM> ItemLikes { get; set; }

        public IList<CommentVM> ItemComments { get; set; }

        public UserVM Owner { get; set; }

        public bool Liked { get; set; }
    }
}