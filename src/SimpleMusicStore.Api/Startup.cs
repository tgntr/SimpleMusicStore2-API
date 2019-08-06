using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using StackExchange.Redis;
using SimpleMusicStore.Data;
using SimpleMusicStore.ModelValidations;
using SimpleMusicStore.BackgroundServiceProvider;
using SimpleMusicStore.Contracts.BackgroundServiceProvider;
using Microsoft.AspNetCore.Authentication.Cookies;
using SimpleMusicStore.Auth;
using SimpleMusicStore.EmailSender;
using SimpleMusicStore.Api.StartupConfigurations;
using Hangfire;
using SimpleMusicStore.Newsletter;
using SimpleMusicStore.Contracts.Newsletter;

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCustomServices(Configuration);
            services.AddRepositories();
            services.AddSingleton(RedisDatabase());
            services.AddBackgroundServiceProvider();
            services.AddGoogleAuthentication(GoogleAuthClientId());
            services.AddNewsletter(EmailSenderCredentials());
            services.AddHangfire(HangfireConnectionString());

            
            
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

            app.UseHangfireDashboard();
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
        private string GoogleAuthClientId() => Configuration["GoogleAuth:ClientId"];
        private IConfigurationSection EmailSenderCredentials() => Configuration.GetSection("EmailSender");

        private string DbConnectionString() => Configuration["Database:Connection"];
        private string HangfireConnectionString() => Configuration["Hangfire:Connection"];

        private IDatabase RedisDatabase() => ConnectionMultiplexer.Connect(Configuration["Redis:Connection"]).GetDatabase();
    }
}
