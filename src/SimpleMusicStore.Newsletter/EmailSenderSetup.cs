using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Newsletter;
using SimpleMusicStore.Models.Credentials;
using SimpleMusicStore.Newsletter;

namespace SimpleMusicStore.EmailSender
{
    public static class EmailSenderSetup
    {
        public static void AddNewsletter(this IServiceCollection services, IConfigurationSection credentials)
        {
            services.AddSingleton(credentials.Get<EmailSenderCredentials>());
            services.AddScoped<Notificator, EmailSender>();
            services.AddScoped<NewsletterGenerator, LatestFromFavorites>();

        }
    }
}
