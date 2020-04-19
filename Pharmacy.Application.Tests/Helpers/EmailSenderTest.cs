using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Tests.BuiltinServicesMocks;
using Pharmacy.Infrastructure.Services;

namespace Pharmacy.Application.Tests.Helpers
{
    [TestFixture]
    public class EmailSenderTest
    {
        [Test]
        public void SendEmail_DoesNotThrowSendEmailException()
        {
            var configMock = IConfigMock.MockIConfigurationEmailSection();
            var configMockObject = configMock.Object;

            var receiver = configMockObject["Sender"];
            var subject = configMockObject["ConfirmEmailSubject"];
            var body = "<html><b></b></html>";
            var message = "<h1>Test aws email sender</h1>";

            var emailSenderService = new EmailSender(configMockObject);

            Assert.DoesNotThrowAsync(async () => await emailSenderService.SendEmailAsync(receiver, subject, body, message));
        }
    }
}
