﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theFungiApplication.DataTransfer
{
    public class CollectionItemInfoCreateDto
    {
        public int Id { get; set; } //For Update
        public string Title { get; set; }
        public string Content { get; set; }
        public int CollectionItemId { get; set; }
    }
}
