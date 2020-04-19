using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Configuration;
using Pharmacy.Infrastructure.Interfaces;
using System.Collections.Generic;
using Pharmacy.Application.Common.Exceptions;
using System.Threading.Tasks;
using Pharmacy.Infrastructure.Constants;

namespace Pharmacy.Infrastructure.Services
{
    public class EmailSender: IEmailSender
    {
        private readonly IConfiguration _configuration;

        private readonly string _sender;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _sender = _configuration["EmailSettings:Sender"];
        }

        public async Task SendEmailAsync(string email, string subject, string body, string message)
        {
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUWest1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = _sender,
                    Destination = new Destination
                    {
                        ToAddresses =
                        new List<string> { email }
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = body
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = message
                            }
                        }
                    }
                };

               var response = await client.SendEmailAsync(sendRequest);

               if (response.HttpStatusCode != System.Net.HttpStatusCode.Accepted)
                   throw new SendEmailException(ExceptionConstants.SendEmailException, email, _sender);
            }
        }
    }
}
