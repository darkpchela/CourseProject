﻿using System.Collections.Generic;

namespace CourseProject.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public IList<ItemOptionalFieldVM> Fields { get; set; }
    }
}