using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.MedicamentIn;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
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
        private readonly IMapper _mapper;
        private readonly IAllowedForEntityService _allowedForEntityService;
        private readonly IBlobStorage _blobStorage;
        private readonly IInstructionService _instructionService;
        private readonly ILogger<MedicamentController> _logger;

        public MedicamentController(IMedicamentService medicamentService, IBlobStorage blobStorage,
                                    IAllowedForEntityService allowedForEntityService, ILogger<MedicamentController> logger,
                                    IMapper mapper, IInstructionService instructionService)
        {
            _medicamentService = medicamentService;
            _allowedForEntityService = allowedForEntityService;
            _instructionService = instructionService;
            _blobStorage = blobStorage;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMedicament(MedicamentInDto medicamentDto)
        {
            try
            {
                return Ok();
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