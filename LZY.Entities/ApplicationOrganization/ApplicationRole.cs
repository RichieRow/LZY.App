﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LZY.Entities.ApplicationOrganization
{
    public class ApplicationRole : IdentityRole
    {
        [StringLength(250)]
        public string DisplayName { get; set; }
        [StringLength(550)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }
        public ApplicationRoleTypeEnum ApplicationRoleType { get; set; } 

        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
