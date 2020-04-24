using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
