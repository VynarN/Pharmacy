using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Application.Tests.BuiltinServicesMocks
{
    public static class IConfigMock
    {
        public static Mock<IConfiguration> MockIConfigurationEmailSection()
        {
            var configMock = new Mock<IConfiguration>();

            configMock.Setup(meth => meth["EmailSettings:Sender"]).Returns("online.pharmacy.ua@gmail.com");
            configMock.Setup(meth => meth["EmailSettings:ConfirmEmailSubject"]).Returns("Email confirmation");
            configMock.Setup(meth => meth["EmailSettings:ResetPasswordSubject"]).Returns("Reset password");
            configMock.Setup(meth => meth["EmailSettings:ConfirmEmailPathTemplate"]).Returns("\\ConfirmEmailTemplate.html");
            configMock.Setup(meth => meth["EmailSettings:ResetPasswordPathTemplate"]).Returns("\\ResetPasswordTemplate.html");
            configMock.Setup(meth => meth["SendGrid:User"]).Returns("Online Pharmacy");
            configMock.Setup(meth => meth["SendGrid:Key"]).Returns("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");
            configMock.Setup(meth => meth["SendGrid:Sender"]).Returns("online.pharmacy.ua@gmail.com");

            return configMock;
        }
    }
}
