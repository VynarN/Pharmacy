using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Validators;
using System.IO;
using System.Threading.Tasks;

namespace Pharmacy.Application.Helpers
{
    public class EmailHelper: IEmailHelper
    {
        private readonly IConfiguration _configuration;

        private readonly IEmailSender _emailSender;

        private readonly IWebHostEnvironment _environment;

        public EmailHelper(IConfiguration configuration, IEmailSender emailSender, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _emailSender = emailSender;
            _environment = environment;
        }

        public async Task Send(
            string emailReceiver,
            string PathToEmailBodyTemplate,
            string emailSubjectTemplate,
            params string[] links)
        {
            StringArgumentValidator.ValidateStringArgument(emailReceiver, nameof(emailReceiver));
            StringArgumentValidator.ValidateStringArgument(PathToEmailBodyTemplate, nameof(PathToEmailBodyTemplate));
            StringArgumentValidator.ValidateStringArgument(emailSubjectTemplate, nameof(emailSubjectTemplate));

            var pathToTemplate = _environment.WebRootPath + _configuration[PathToEmailBodyTemplate];

            string content = string.Empty;

            using (TextReader reader = new StreamReader(pathToTemplate))
            {
                content = await reader.ReadToEndAsync();
            }

            var formatedTemplateContent = string.Format(content, links);

            var emailSubject = _configuration[emailSubjectTemplate];

            await _emailSender.SendEmailAsync(emailReceiver, emailSubject, formatedTemplateContent);
        }
    }
}
