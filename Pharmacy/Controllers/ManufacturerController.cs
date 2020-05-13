using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
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
        private readonly IAddressService _addressService;
        private readonly ILogger<ManufacturerController> _logger;

        public ManufacturerController(IMapper mapper, IManufacturerService manufacturerService, 
                                      IAddressService addressService, ILogger<ManufacturerController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _manufacturerService = manufacturerService;
            _addressService = addressService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateManufacturer(ManufacturerInDto manufacturerDto)
        {
            try
            {
                var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);

                var address = _mapper.Map<Address>(manufacturerDto.Address);

                var createdManufacturerId = await _manufacturerService.CreateManufacturer(manufacturer);

                address.ManufacturerId = createdManufacturerId;

                await _addressService.CreateAddress(address);

                return Ok();
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