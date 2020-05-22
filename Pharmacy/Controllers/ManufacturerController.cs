using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.DTO.In.ManufacturerIn;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IManufacturerService _manufacturerService;
        private readonly ILogger<ManufacturerController> _logger;

        public ManufacturerController(IMapper mapper, IManufacturerService manufacturerService, ILogger<ManufacturerController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _manufacturerService = manufacturerService;
        }

        [Authorize(Roles = "manager,admin,mainadmin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateManufacturer(ManufacturerInDto manufacturerDto)
        {
            try
            {
                var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);

                var createdManufacturerId = await _manufacturerService.CreateManufacturer(manufacturer);

                return Ok(createdManufacturerId);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}