using Pharmacy.Application.Common.DTO;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IInstructionService
    {
        Task CreateInstruction(InstructionDto instructionDto, int medicamentId);

        Task UpdateInstruction(InstructionDto instructionDto, int medicamentId);
    }
}
