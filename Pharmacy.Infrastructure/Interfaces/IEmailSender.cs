using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string body, string message);
    }
}
