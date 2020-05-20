using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class InstructionService: IInstructionService
    {
        private readonly IRepository<Instruction> _repository;

        public InstructionService(IRepository<Instruction> repository)
        {
            _repository = repository;
        }

        public async Task CreateInstruction(Instruction instruction)
        {
            await _repository.Create(instruction);

        }

        public async Task UpdateInstruction(Instruction instruction)
        {
            await _repository.Update(instruction);
        }
    }
}
