using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimpleMusicStore.Api.StartupConfigurations;
using SimpleMusicStore.Auth.Extensions;
using SimpleMusicStore.Data;
using SimpleMusicStore.EmailSender;
using SimpleMusicStore.ModelValidations;
using SimpleMusicStore.Newsletter;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen.ConventionalRouting;
using System.Linq;

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
                .AddControllers(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add(typeof(ValidateModelStateGloballyAttribute));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDatabase(DbConnectionString());
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCustomServices(Configuration);
            services.AddRepositories();
            services.AddSingleton(RedisDatabase());
            services.AddJwtAuthentication(JwtPayload());
            services.AddNewsletter(EmailSenderCredentials());
            services.AddHangfire(HangfireConnectionString());
            services.AddSwaggerGen(s => 
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Enter JWT token value",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } });
            });//(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleMusicStore.API", Version = "v1" }));
            services.AddSwaggerGenWithConventionalRoutes(options =>
            {
                options.IgnoreTemplateFunc = (template) => template.StartsWith("api/");
                options.SkipDefaults = true;
            });



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
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleMusicStore.API"));
            app.UseHangfireDashboard();
            app.ConfigureExceptionHandler();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
                routes.MapDefaultControllerRoute();
                ConventionalRoutingSwaggerGen.UseRoutes(routes);
            });
        }

        private string GoogleAuthClientId() => Configuration["GoogleAuth:ClientId"];
        private IConfigurationSection EmailSenderCredentials() => Configuration.GetSection("EmailSender");

        private IConfigurationSection JwtPayload() => Configuration.GetSection("JwtPayload");

        private string DbConnectionString() => Configuration["Database:Connection"];
        private string HangfireConnectionString() => Configuration["Hangfire:Connection"];

        private IDatabase RedisDatabase() => ConnectionMultiplexer.Connect(Configuration["Redis:Connection"]).GetDatabase();
    }
}
