using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace bikeshop.EmailServices.EmailService
{
    public class MailKitEmailSender : IEmailSender
    {
        private string _emailCompany;
        private string _passwordCompany;
        public MailKitEmailSender(string emailCompany, string passwordCompany)
        {
            _emailCompany = emailCompany;
            _passwordCompany = passwordCompany;
        }

        public async Task SendEmailAsync(string email, string subject, string textMessage)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Seller", "seller@bikeshop.com"));
            message.To.Add(new MailboxAddress("Client", email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = textMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync(_emailCompany, _passwordCompany);
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}