using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IInstructionService
    {
        Task CreateInstruction(Instruction instruction);

        Task UpdateInstruction(Instruction instruction);
    }
}
