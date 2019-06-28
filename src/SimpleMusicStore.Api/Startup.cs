using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Api.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using StackExchange.Redis;
using SimpleMusicStore.Data;
using SimpleMusicStore.ModelValidations;
using SimpleMusicStore.BackgroundServiceProvider;
using SimpleMusicStore.Contracts.BackgroundServiceProvider;
using Microsoft.AspNetCore.Authentication.Cookies;
using SimpleMusicStore.Auth;

namespace SimpleMusicStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.Filters.Add(typeof(ValidateModelStateGloballyAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDatabase(DbConnectionString());
            services.AddCustomServices(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(RedisDatabase());
            services.AddBackgroundServiceProvider();
            services.AddGoogleAuthentication(GoogleAuthCredentials());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
        private IConfigurationSection GoogleAuthCredentials() => Configuration.GetSection("GoogleAuth");

        private string DbConnectionString() => Configuration["Database:Connection"];

        private IDatabase RedisDatabase() => ConnectionMultiplexer.Connect(Configuration["Redis:Connection"]).GetDatabase();
    }
}
