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

            configMock.Setup(meth => meth["Sender"]).Returns("online.pharmacy.ua@gmail.com");
            configMock.Setup(meth => meth["ConfirmEmailSubject"]).Returns("Email confirmation");
            configMock.Setup(meth => meth["ResetPasswordSubject"]).Returns("Reset password");
            configMock.Setup(meth => meth["ConfirmEmailPathTemplate"]).Returns("\\ConfirmEmailTemplate.html");
            configMock.Setup(meth => meth["ResetPasswordPathTemplate"]).Returns("\\ResetPasswordTemplate.html");
            configMock.Setup(meth => meth["EmailBodyPath"]).Returns("\\EmailBody.html");

            return configMock;
        }
    }
}
