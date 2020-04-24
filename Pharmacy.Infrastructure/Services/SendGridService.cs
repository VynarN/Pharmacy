using Microsoft.Extensions.Configuration;
using Pharmacy.Infrastructure.Common.Constants;
using Pharmacy.Infrastructure.Common.Exceptions;
using Pharmacy.Infrastructure.Common.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Services
{
    public class SendGridService: IEmailSender
    {
        private readonly IConfiguration Configuration;

        public SendGridService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var response = await Execute(Configuration["SendGrid:Key"], subject, body, email);

            if (response.StatusCode != HttpStatusCode.Accepted)
                throw new SendEmailException(ExceptionConstants.SendEmailException + " " + response.StatusCode, email);
        }

        private Task<Response> Execute(string apiKey, string subject, string body, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Configuration["SendGrid:Sender"], Configuration["SendGrid:User"]),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
