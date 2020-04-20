using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Pharmacy.Application.Tests.BuiltinServicesMocks;
using Pharmacy.Infrastructure.Services;

namespace Pharmacy.Application.Tests.Services
{
    [TestFixture]
    public class EmailSenderTest
    {
        [Test]
        public void SendEmail_DoesNotThrowSendEmailException()
        {
            var configMock = IConfigMock.MockIConfigurationEmailSection();
            var configMockObject = configMock.Object;

            var receiver = configMockObject["SendGrid.Sender"];
            var subject = configMockObject["EmailSettings:ConfirmEmailSubject"];
            var body = "<html><b><h1> Test SendGrid service</h1></b></html>";

            var emailSenderService = new SendGridService(configMockObject);

            Assert.DoesNotThrowAsync(async () => await emailSenderService.SendEmailAsync(receiver, subject, body));
        }
    }
}
