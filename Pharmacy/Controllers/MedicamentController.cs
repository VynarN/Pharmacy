using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.MedicamentIn;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Domain.Entites;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly IMedicamentService _medicamentService;
        private readonly IAllowedForEntityService _allowedForEntityService;
        private readonly IInstructionService _instructionService;
        private readonly ILogger<MedicamentController> _logger;
        private readonly IMapper _mapper;

        public MedicamentController(IMedicamentService medicamentService, ILogger<MedicamentController> logger,
                                    IAllowedForEntityService allowedForEntityService, IMapper mapper,
                                    IInstructionService instructionService)
        {
            _medicamentService = medicamentService;
            _allowedForEntityService = allowedForEntityService;
            _instructionService = instructionService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMedicament(MedicamentInDto medicamentDto)
        {
            try
            {
                var allowedForEntity = _mapper.Map<AllowedForEntity>(medicamentDto.AllowedForEntity);

                var instruction = _mapper.Map<Instruction>(medicamentDto.Instruction);

                var medicament = _mapper.Map<Medicament>(medicamentDto);

                medicament.AllowedForEntityId = await _allowedForEntityService.CreateAllowedForEntity(allowedForEntity);

                var createdMedicamentId = await _medicamentService.CreateMedicament(medicament);

                instruction.MedicamentId = createdMedicamentId;

                await _instructionService.CreateInstruction(instruction);

                return Ok(createdMedicamentId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }
    }
}