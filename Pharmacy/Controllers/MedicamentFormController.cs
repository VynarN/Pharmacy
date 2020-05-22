using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.DTO.Out;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentFormController : ControllerBase
    {
        private readonly IMedicamentFormService _medicamentFormService;
        private readonly IMapper _mapper;
        private readonly ILogger<MedicamentFormController> _logger;

        public MedicamentFormController(IMedicamentFormService medicamentFormService, IMapper mapper, ILogger<MedicamentFormController> logger)
        {
            _medicamentFormService = medicamentFormService;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Roles = "manager,admin,mainadmin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateMedicamentForm(string medicamentForm)
        {
            try
            {
                await _medicamentFormService.CreateMedicamentForm(medicamentForm);

                return Ok();
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get")]
        public IActionResult GetMedicamentForms()
        {
            try
            {
                var medicamentForms = _medicamentFormService.GetMedicamentForms();

                var mappedMedicamentForms = _mapper.Map<IEnumerable<MedicamentFormOutDto>>(medicamentForms);

                return Ok(mappedMedicamentForms);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}