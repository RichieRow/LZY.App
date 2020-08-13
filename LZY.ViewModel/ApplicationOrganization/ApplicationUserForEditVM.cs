using LZY.Common.JsonModels;
using LZY.Common.ViewModelComponents;
using LZY.Entities.ApplicationOrganization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LZY.ViewModels.ApplicationOrganization
{
    public class ApplicationUserForEditVM : EntityBaseVM
    {
        public string NickName { get; set; } 

        public string UserName { get; set; } 
 
        public string MobileNumber { get; set; }
 
        public string EMail { get; set; } 

        public bool LockoutEnabled { get; set; } 
        public Guid RoleId { get; set; } 

        public ApplicationUserForEditVM()
        { }
        public ApplicationUserForEditVM(ApplicationUser bo,Guid roleId)
        {
     
            this.Id = Guid.Parse(bo.Id);
            this.UserName = bo.UserName;
            this.MobileNumber = bo.MobileNumber;
            this.EMail = bo.Email;
            this.NickName = bo.NickName;
            this.LockoutEnabled = bo.LockoutEnabled;
            this.RoleId = roleId; 

        }

    }
}
