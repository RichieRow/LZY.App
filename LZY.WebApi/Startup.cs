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

            // ��� EF Core ��ܣ����Ӵ���appsettings����
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

            // ���� Identity

            #region redis���
            //ConnectionMultiplexer.Connect("Redis1_IP��ַ:�˿�,password=����");
            var url = Configuration["RedisUrl"];
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(url));
            // services.AddSingleton<RedisHelper>();
            //�ֲ�ʽ����
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = url;
                options.InstanceName = "LZY.API";
            });
            #endregion
            //����
            services.AddCors(options =>
            {
                options.AddPolicy(cors, builder =>
                {
                    builder.AllowAnyOrigin() //�����κ���Դ����������
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//ָ������cookie
                });
            });
            //ע��IdentityServer 
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
