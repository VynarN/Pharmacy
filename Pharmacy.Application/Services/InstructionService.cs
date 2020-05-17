using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System;
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

        public Task UpdateInstruction(Instruction instruction, int instructionId)
        {
            throw new NotImplementedException();
        }
    }
}
