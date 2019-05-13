using SimpleMusicStore.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Services;
using SimpleMusicStore.Models.AuthenticationProviders;
using SimpleMusicStore.Auth.Extensions;
using SimpleMusicStore.MusicLibrary;
using SimpleMusicStore.Storage;
using SimpleMusicStore.Contracts.Repositories;
using SimpleMusicStore.Repositories;

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
			//TODO Environment class to access appsettings values
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.Configure<JwtConfiguration>(JwtPayloadSection());
			services.AddJwtAuthentication(JwtConfiguration());
			services.AddScoped<AuthenticationHandler, Jwt>();
			services.AddScoped<IdentityHandler, UserManager>();
			services.AddScoped<MusicSource, Discogs>();
			services.AddScoped<FileStorage, GoogleCloud>();
            services.AddScoped<IUserRepository, UserRepository>();
			services.AddAutoMapper();
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

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();

        }
		private IConfigurationSection JwtPayloadSection()
		{
			return Configuration.GetSection("JwtPayload");
		}

		private JwtConfiguration JwtConfiguration()
		{
			return JwtPayloadSection().Get<JwtConfiguration>();
		}


		
	}
}
