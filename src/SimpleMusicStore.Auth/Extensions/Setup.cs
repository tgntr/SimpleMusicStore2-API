using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Models.AuthenticationProviders;

namespace SimpleMusicStore.Auth.Extensions
{
	public static class Setup
	{
		public static void AddJwtAuthentication(this IServiceCollection services, JwtConfiguration config)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
					{
						//TODO should i move the lambda functions in the configurations extension?
						options.RequireHttpsMetadata = false;
						options.SaveToken = true;
						options.TokenValidationParameters = config.ValidationParameters();
					});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("ApiUser", policy => policy.RequireClaim("username"));
			});
		}
	}
}
