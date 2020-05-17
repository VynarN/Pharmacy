using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Services
{
    public class SendGridService: IEmailSender
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<SendGridService> _logger;

        public SendGridService(IConfiguration configuration, ILogger<SendGridService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var response = await Execute(_configuration["SendGrid:Key"], subject, body, email);

            if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogWarning(await response.Body.ReadAsStringAsync(), response.StatusCode.ToString());
                throw new SendEmailException(ExceptionStrings.SendEmailException + " " + response.StatusCode, email);
            }
        }

        private Task<Response> Execute(string apiKey, string subject, string body, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_configuration["SendGrid:Sender"], _configuration["SendGrid:User"]),
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
