using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleMusicStore.Models.AuthenticationProviders;

namespace SimpleMusicStore.Auth.Extensions
{
	public static class JwtExtensions
	{
		public static void AddJwtAuthentication(this IServiceCollection services, JwtTokenConfiguration token)
		{
			var secret = token.SecretEncoded();

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secret),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
		}
	}
}
