﻿using System.Collections.Generic;

namespace BusinessLayer.Models
{
    public class GetResourceResultModel
    {
        public bool Succeed { get; private set; } = true;

        public string Uri { get; set; }

        public IList<string> Errors { get; set; } = new List<string>();

        public void AddError(string error)
        {
            if (Succeed)
                Succeed = false;
            Errors.Add(error);
        }
    }
}