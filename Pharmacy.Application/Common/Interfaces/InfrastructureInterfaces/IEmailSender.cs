using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
