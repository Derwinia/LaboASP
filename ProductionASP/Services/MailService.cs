using ProductionASP.Services;
using System.Net;
using System.Net.Mail;

namespace ProductionASP.Services
{
    public class MailService
    {
        private MailConfig _config;
        private SmtpClient _client;

        public MailService(MailConfig config, SmtpClient client)
        {
            _config = config;
            _client = client;
            _client.Host = config.Host;
            _client.Port = config.Port;
            _client.Credentials = new NetworkCredential(
                config.Email, config.Password
            );
            _client.EnableSsl = true;
        }

        public void Send(string subject, string content)
        {
            MailMessage message = new();
            message.From = new MailAddress(_config.Email);
            message.To.Add(_config.AdminMail);
            message.Subject = subject;
            message.Body = content;
            _client.Send(message);
        }


    }
}
