﻿using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
    }
}
