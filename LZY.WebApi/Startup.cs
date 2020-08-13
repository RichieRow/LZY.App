using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using LZY.DataAccess;
using LZY.DataAccess.Seeds;
using LZY.DataAccess.SqlServer;
using LZY.DataAccess.SqlServerr;
using LZY.Entities.ApplicationOrganization;
using LZY.Entities.Attachments;
using LZY.Entities.BusinessManagement.Audit;
using LZY.Entities.Site;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace LZY.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private string cors = "any";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // 添加 EF Core 框架，连接串在appsettings设置
            services.AddDbContext<EntityDbContext>(d => d.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //   .AddEntityFrameworkStores<EntityDbContext>()
            //   .AddDefaultTokenProviders();

            services.AddTransient<IEntityRepository<AuditRecord>, EntityRepository<AuditRecord>>();
            services.AddTransient<IEntityRepository<BusinessFile>, EntityRepository<BusinessFile>>();
            services.AddTransient<IEntityRepository<BusinessImage>, EntityRepository<BusinessImage>>();
            services.AddTransient<IEntityRepository<BusinessVideo>, EntityRepository<BusinessVideo>>();
            services.AddTransient<IEntityRepository<FriendLink>, EntityRepository<FriendLink>>();
            services.AddTransient<IEntityRepository<About>, EntityRepository<About>>();
            services.AddTransient<IEntityRepository<Message>, EntityRepository<Message>>();
            services.AddTransient<IEntityRepository<SiteSetting>, EntityRepository<SiteSetting>>();
            services.AddTransient<IEntityRepository<Banner>, EntityRepository<Banner>>();

            // 配置 Identity

            #region redis相关
            //ConnectionMultiplexer.Connect("Redis1_IP地址:端口,password=密码");
            var url = Configuration["RedisUrl"];
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(url));
            // services.AddSingleton<RedisHelper>();
            //分布式缓存
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = url;
                options.InstanceName = "LZY.API";
            });
            #endregion
            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy(cors, builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            //注册IdentityServer 
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:5000";
                        options.ApiName = "api1";
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "mvc secret";
                    });
       

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EntityDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            DbInitializer.Initialize(context);
        }
    }
}
