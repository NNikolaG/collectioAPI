﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theFungiDomain.Entities
{
    public class Roles : Entity
    {
        public virtual ICollection<RoleUseCases> UseCases { get; set; }
    }
}
