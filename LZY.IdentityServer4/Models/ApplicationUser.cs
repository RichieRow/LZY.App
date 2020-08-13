using Microsoft.AspNetCore.Identity;

namespace LZY.IdentityServer4.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
        public string MobileNumber { get; set; }
        public string Avatar { get; set; }
        public string Remark { get; set; }
    }
}
