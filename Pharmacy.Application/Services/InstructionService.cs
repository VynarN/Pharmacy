using AutoMapper;
using Pharmacy.Application.Common.DTO;
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

        private readonly IMapper _mapper;

        public InstructionService(IRepository<Instruction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateInstruction(InstructionDto instructionDto, int medicamentId)
        {
            var instruction = _mapper.Map<Instruction>(instructionDto);

            instruction.MedicamentId = medicamentId;

            await _repository.Create(instruction);
        }

        public async Task UpdateInstruction(InstructionDto instructionDto, int medicamentId)
        {
            throw new NotImplementedException();
        }
    }
}
