using LZY.DataAccess.SqlServer;
using LZY.Entities.ApplicationOrganization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LZY.Entities.Site;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LZY.DataAccess.Seeds
{
    /// <summary>
    /// 构建一个初始化原始数据的组件，用于程序启动的时候执行一些数据初始化的操作
    /// </summary>
    public static class DbInitializer
    {
        static EntityDbContext _Context;

        public static void Initialize(EntityDbContext context)
        {
            _Context = context;
            context.Database.EnsureCreated();

            _AddApplicationRole();
            _AddDefaultData();
        }
        private static void _AddApplicationRole()
        {
            if (_Context.Roles.Any()) return;
            var roles = new List<ApplicationRole>()
            {
               new ApplicationRole(){Name="Admin",DisplayName="系统管理", Description="适用于系统管理人员",ApplicationRoleType=ApplicationRoleTypeEnum.适用于系统管理人员},
               new ApplicationRole(){Name="User",DisplayName="普通注册用户", Description="适用于普通注册用户",ApplicationRoleType=ApplicationRoleTypeEnum.适用于普通注册用户}
            };
            _Context.ApplicationRoles.AddRange(roles);
            _Context.SaveChanges();
        }

        private static void _AddDefaultData()
        {
            //站点信息
            if (!_Context.SiteSettings.Any())
            {
                var siteSetting = new SiteSetting()
                {
                    Title = "我的博客",
                    Description = "这是我的个人博客",
                    Keywords = "个人,技术,博客",
                    Logo = "/images/logo.png",
                    Statistics = "<span id='cnzz_stat_icon_1278811683'></span><script src='https://v1.cnzz.com/z_stat.php?id=1278811683&online=1&show=line' type='text/javascript'></script>",
                    UseImgLogo = false,
                    Copyright = "这里填写网站版权信息",
                    ICP = "这是填写博客站点的备案号",
                    IsOpen = true,
                    QrCode= "/images/qrCode/qrcode.png"
                };
                _Context.SiteSettings.Add(siteSetting);
            }
            // 关于我
            if (!_Context.Abouts.Any())
            {
                var about = new About()
                {
                    Content = "<div><h1>这里是关于我的内容</h1><p>这里支持Html元素</p> </div>"
                };
                _Context.Abouts.Add(about);
            }

            // 轮播图
            if (!_Context.Banners.Any())
            {
                var banners = new List<Banner>()
                {
                    new Banner(){IsBanner=true, Title="首页轮播图名称1",Description="首页轮播图描述",Url="/images/main/article_image_002.jpg",Href="http://超链接",IsShow=true},
                    new Banner(){IsBanner=true, Title="首页轮播图名称2",Description="首页轮播图描述",Url="/images/main/article_image_001.jpg",Href="http://超链接",IsShow=true},
                    new Banner(){IsBanner=true, Title="首页轮播图名称3",Description="首页轮播图描述",Url="/images/main/article_image_002.jpg",Href="http://超链接",IsShow=true},
                    new Banner(){IsBanner=false, Title="广告名称1",Description="广告描述",Url="/images/main/article_image_001.jpg",Href="http://超链接",IsShow=true}
                };
                _Context.Banners.AddRange(banners);
            }

            _Context.SaveChanges();
        }
    }
}
