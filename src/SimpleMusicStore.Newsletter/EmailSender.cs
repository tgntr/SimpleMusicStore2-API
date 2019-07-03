using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using SimpleMusicStore.Contracts.Newsletter;
using SimpleMusicStore.Models.Credentials;
using System.Threading.Tasks;

namespace SimpleMusicStore.EmailSender
{
    public class EmailSender : Notificator
    {
        private readonly EmailSenderCredentials _credentials;

        public EmailSender(EmailSenderCredentials credentials)
        {
            _credentials = credentials;
        }

        public Task Send(string subscriber, string news)
        {
            var message = CreateMessage(subscriber, news);
            return SendMessage(message);
        }

        private async Task SendMessage(MimeMessage message)
        {
            using (var emailClient = new SmtpClient())
            {
                await emailClient.ConnectAsync(_credentials.Server, _credentials.Port, true);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await emailClient.AuthenticateAsync(_credentials.Username, _credentials.Password);
                await emailClient.SendAsync(message);
                await emailClient.DisconnectAsync(true);
            }
        }

        private MimeMessage CreateMessage(string subscriber, string news)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(subscriber));
            message.From.Add(new MailboxAddress(_credentials.Username));
            message.Subject = "Latest from your favorite artists and labels";

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = news
            };
            return message;
        }
    }
}
