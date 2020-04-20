using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Common.Validators;
using Pharmacy.Infrastructure.Common.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Pharmacy.Application.Helpers
{
    public static class EmailHelper
    {
        public static async Task SendEmail(
            string emailReceiver,
            string emailContentPathTemplate,
            string emailSubjectTemplate,
            string rootPath,
            IConfiguration configuration,
            IEmailSender emailService,
            params string[] links)
        {
            StringArgumentValidator.ValidateStringArgument(emailReceiver, nameof(emailReceiver));
            StringArgumentValidator.ValidateStringArgument(emailContentPathTemplate, nameof(emailContentPathTemplate));
            StringArgumentValidator.ValidateStringArgument(emailSubjectTemplate, nameof(emailSubjectTemplate));
            StringArgumentValidator.ValidateStringArgument(rootPath, nameof(rootPath));

            var pathToEmailContentTemplate = rootPath + configuration[emailContentPathTemplate];

            var content = File.ReadAllText(pathToEmailContentTemplate);

            var formatedTemplateContent = string.Format(content, links);

            var emailSubject = configuration[emailSubjectTemplate];

            await emailService.SendEmailAsync(emailReceiver, emailSubject, formatedTemplateContent);
        }
    }
}
