using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LZY.Entities.ApplicationOrganization;
using LZY.Entities.Attachments;
using LZY.Entities.BusinessManagement.Audit;
using System;
using System.Collections.Generic;
using System.Text;
using LZY.Entities;
using LZY.Entities.Site;

namespace LZY.DataAccess.SqlServer
{
    public class EntityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
        }  
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } 
        public DbSet<AuditRecord> AuditRecords { get; set; }
        public DbSet<BusinessFile> BusinessFiles { get; set; }
        public DbSet<BusinessImage> BusinessImages { get; set; }
        public DbSet<BusinessVideo> BusinessVideos { get; set; }  
        public DbSet<FriendLink> FriendLinks { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<Banner> Banners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Person>().ToTable("Person");
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
