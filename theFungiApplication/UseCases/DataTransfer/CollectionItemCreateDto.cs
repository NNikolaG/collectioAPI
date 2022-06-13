﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theFungiApplication.DataTransfer
{
    public class CollectionItemCreateDto
    {
        public int Id { get; set; } //For Update
        public string Title { get; set; }
        public int CollectionId { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }

    }
}
