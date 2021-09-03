using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;
using Microsoft.AspNet.Identity;

namespace intSoft.MVC.Core.Utilities
{
    public class EmailService : IIdentityMessageService
    {
        public virtual Task SendAsync(IdentityMessage message)
        {
            return ConfigSendGridasync(message);
        }

        private async Task ConfigSendGridasync(IdentityMessage message)
        {
            var configuration = DependencyResolver.Current.GetService<ApplicationConfiguration>();
            var client = new SmtpClient
            {
                Host = configuration.EmailProviderHostAddress,
                EnableSsl = configuration.EmailProviderUseSSL,
                Port = configuration.EmailProviderPort,
                Credentials =
                    new NetworkCredential(configuration.EmailProviderUsername, configuration.EmailProviderPassword)
            };
            await Task.Run(() =>
            {
                client.Send(new MailMessage
                {
                    IsBodyHtml = true,
                    Body = message.Body,
                    Subject = message.Subject,
                    To = { message.Destination },
                    From = new MailAddress(configuration.ActivationEmailFromAddress, "Accounts")
                });
            });

        }
    }
}