using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.HelpersInterfaces
{
    public interface IEmailHelper
    {
        Task Send(string emailReceiver, string PathToEmailBodyTemplate, string emailSubjectTemplate, params string[] links);
    }
}
