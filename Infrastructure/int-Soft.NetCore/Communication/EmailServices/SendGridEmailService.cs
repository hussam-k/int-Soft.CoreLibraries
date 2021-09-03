using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace int_Soft.NetCore.Communication.EmailServices
{
    public class SendGridEmailService : IMessageService
    {
        public Task<MessageServiceResult> SendAsync(Message message)
        {
            bool enableSsl;
            int port;
            string host = ConfigurationManager.AppSettings[CommunicationMagicStrings.SendGridHost];
            string username = ConfigurationManager.AppSettings[CommunicationMagicStrings.SendGridUsername];
            string password = ConfigurationManager.AppSettings[CommunicationMagicStrings.SendGridPassword];

            if (string.IsNullOrEmpty(host)
                || string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password)
                || !bool.TryParse(ConfigurationManager.AppSettings[CommunicationMagicStrings.SendGridEnableSsl], out enableSsl)
                || !int.TryParse(ConfigurationManager.AppSettings[CommunicationMagicStrings.SendGridPort], out port))
                throw new Exception("SendGrid service configuration is missed!");

            var client = new SmtpClient
            {
                Host = host,
                EnableSsl = enableSsl,
                Port = port,
                Credentials = new NetworkCredential(username, password)
            };
            try
            {
                client.Send(new MailMessage
                {
                    IsBodyHtml = true,
                    Body = message.Body,
                    Subject = message.Subject,
                    To = { message.Destination },
                    From = new MailAddress(message.From, message.FromDisplayText)
                });
                return Task.FromResult(MessageServiceResult.Success());
            }
            catch (Exception ex)
            {
                return Task.FromResult(MessageServiceResult.Error(ex.Message));
            }
        }
    }
}