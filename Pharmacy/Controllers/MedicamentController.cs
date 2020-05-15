using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.MedicamentIn;
using Pharmacy.Application.Common.DTO.Out;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Queries;
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
        private readonly IPaginationHelper _paginationHelper;
        private readonly ILogger<MedicamentController> _logger;
        private readonly IMapper _mapper;

        public MedicamentController(IMedicamentService medicamentService, ILogger<MedicamentController> logger,
                                    IAllowedForEntityService allowedForEntityService, IMapper mapper,
                                    IInstructionService instructionService, IPaginationHelper paginationHelper)
        {
            _medicamentService = medicamentService;
            _allowedForEntityService = allowedForEntityService;
            _instructionService = instructionService;
            _paginationHelper = paginationHelper;
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

        [HttpGet("get")]
        public IActionResult GetAll([FromQuery]PaginationQuery paginationQuery,[FromQuery]MedicamentFilterQuery medicamentFilterQuery)
        {
            try
            {
                var medicaments = _medicamentService.GetMedicaments(out int totalMedicamentsCount, paginationQuery);

                var medicamentsDto = medicaments.ProjectTo<MedicamentOutDto>(_mapper.ConfigurationProvider);

                var paginatedResponse = _paginationHelper.FormMedicamentsPaginatedResponse(totalMedicamentsCount, 
                                                          medicamentsDto, paginationQuery, medicamentFilterQuery);

                return Ok(paginatedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }
    }
}