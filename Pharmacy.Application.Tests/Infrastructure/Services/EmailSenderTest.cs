using Microsoft.Extensions.Logging;
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
            var logger = new Mock<ILogger<SendGridService>>();

            var receiver = configMock.Object["SendGrid:Sender"];
            var subject = configMock.Object["EmailSettings:ConfirmEmailSubject"];
            var body = "<html><b><h1> Test SendGrid service</h1></b></html>";

            var emailSenderService = new SendGridService(configMock.Object, logger.Object);

            Assert.DoesNotThrowAsync(async () => await emailSenderService.SendEmailAsync(receiver, subject, body));
        }
    }
}
