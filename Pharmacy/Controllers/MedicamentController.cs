using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.DTO;
using Pharmacy.Application.Common.DTO.In.MedicamentIn;
using Pharmacy.Application.Common.DTO.Out;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly IMedicamentService _medicamentService;
        private readonly IPaginationService _paginationService;
        private readonly ILogger<MedicamentController> _logger;
        private readonly IMapper _mapper;

        public MedicamentController(IMedicamentService medicamentService, ILogger<MedicamentController> logger,
                                    IMapper mapper, IPaginationService paginationService)
        {
            _medicamentService = medicamentService;
            _paginationService = paginationService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Roles = "manager,admin,mainadmin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateMedicament(MedicamentInDto medicamentDto)
        {
            try
            {
                var medicament = _mapper.Map<Medicament>(medicamentDto);

                var createdMedicamentId = await _medicamentService.CreateMedicament(medicament);

                return CreatedAtAction(nameof(Get), new { id = createdMedicamentId }, medicamentDto);
            }
            catch(Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get")]
        public IActionResult GetAll([FromQuery]PaginationQuery paginationQuery,[FromQuery]MedicamentFilterQuery medicamentFilterQuery)
        {
            try
            {
                var medicaments = _medicamentService.GetMedicaments(out int totalMedicamentsCount, paginationQuery, medicamentFilterQuery);

                var medicamentsDto = medicaments.ProjectTo<MedicamentBaseInfoDto>(_mapper.ConfigurationProvider);

                var paginatedResponse = _paginationService.FormMedicamentsPaginatedResponse(totalMedicamentsCount, 
                                                          medicamentsDto, paginationQuery, medicamentFilterQuery);

                return Ok(paginatedResponse);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var medicament = _medicamentService.GetMedicament(id);

                var mappedMedicament = _mapper.Map<MedicamentOutDto>(medicament);

                return Ok(mappedMedicament);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [Authorize(Roles = "manager,admin,mainadmin")]
        [HttpPut("update/{medicamentId}")]
        public async Task<IActionResult> Update(MedicamentUpdateInfoDto medicamentDto, int medicamentId)
        {
            try
            {
                var mappedMedicament = _mapper.Map<Medicament>(medicamentDto);

                await _medicamentService.UpdateMedicament(medicamentId, mappedMedicament);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [Authorize(Roles = "manager,admin,mainadmin")]
        [HttpDelete("delete/{medicamentId}")]
        public async Task<IActionResult> DeleteMedicament(int medicamentId)
        {
            try
            {
                await _medicamentService.DeleteMedicament(medicamentId);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}