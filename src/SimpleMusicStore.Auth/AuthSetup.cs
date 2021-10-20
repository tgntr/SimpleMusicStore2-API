using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleMusicStore.Auth
{
    public static class AuthSetup
    {
        public static void AddGoogleAuthentication(this IServiceCollection services, string clientId)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwt => jwt.UseGoogle(
                    clientId: clientId));
        }
    }
}
