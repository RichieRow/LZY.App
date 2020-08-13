using System;
using System.Collections.Generic;
using System.Text;
using LZY.Entities.Site;

namespace LZY.ViewModels.Site
{ 
    public class AboutVM : EntityBaseVM
    {  
        public string Content { get; set; }
        public AboutVM() { }
        public AboutVM(About about)
        {
            Id = about.Id;
            Content = about.Content; 
        }

    }
}
