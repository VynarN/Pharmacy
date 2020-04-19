using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Common.Validators;
using Pharmacy.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Helpers
{
    public static class EmailSenderTest
    {
        public static async Task SendEmail(
            string emailReceiver,
            string emailBodyPathTempalte,
            string emailContentPathTemplate,
            string emailSubjectSettingsTemplate,
            string rootPath,
            IConfiguration configuration,
            IEmailSender emailService,
            params string[] links)
        {
            StringArgumentValidator.ValidateStringArgument(emailReceiver, nameof(emailReceiver));
            StringArgumentValidator.ValidateStringArgument(emailBodyPathTempalte, nameof(emailBodyPathTempalte));
            StringArgumentValidator.ValidateStringArgument(emailContentPathTemplate, nameof(emailContentPathTemplate));
            StringArgumentValidator.ValidateStringArgument(emailSubjectSettingsTemplate, nameof(emailSubjectSettingsTemplate));
            StringArgumentValidator.ValidateStringArgument(rootPath, nameof(rootPath));

            var pathToEmailContentTemplate = rootPath + configuration[emailContentPathTemplate];
            var pathToEmailBodyTemplate = rootPath + configuration[emailBodyPathTempalte];

            var body = File.ReadAllText(pathToEmailBodyTemplate);
            var content = File.ReadAllText(pathToEmailContentTemplate);

            var formatedTemplateContent = string.Format(content, links);

            var emailSubject = configuration[emailSubjectSettingsTemplate];

            await emailService.SendEmailAsync(emailReceiver, emailSubject, body, formatedTemplateContent);
        }
    }
}
