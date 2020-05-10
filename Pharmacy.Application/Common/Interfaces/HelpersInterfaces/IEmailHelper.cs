using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.HelpersInterfaces
{
    public interface IEmailHelper
    {
        Task Send(string emailReceiver, string PathToEmailBodyTempalte, string emailSubjectTemlate, params string[] links);
    }
}
