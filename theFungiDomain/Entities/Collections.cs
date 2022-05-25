﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theFungiDomain.Entities
{
    public class Collections : Entity
    {
        public int UserId { get; set; }
        public Users User { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
        public virtual ICollection<CollectionItems> CollectionItems { get; set; } = new HashSet<CollectionItems>();
        public virtual ICollection<Follow> CollectionFollowers { get; set; } = new HashSet<Follow>();
    }
}
