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
    public class ApplicationMethodController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationMethodController> _logger;
        private readonly IApplicationMethodService _applicationMethodService;

        public ApplicationMethodController(ILogger<ApplicationMethodController> logger, IApplicationMethodService applicationMethodService, IMapper mapper)
        {
            _applicationMethodService = applicationMethodService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin,mainadmin")]
        [HttpPost("create/{applicationMethod}")]
        public async Task<IActionResult> CreateApplicationMethod(string applicationMethod)
        {
            try
            {
                await _applicationMethodService.CreateApplicationMethod(applicationMethod);

                return Ok();
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get")]
        public IActionResult GetApplicationMethods()
        {
            try
            {
                var methods = _applicationMethodService.GetApplicationMethods();

                var mappedMethods = _mapper.Map<IEnumerable<ApplicationMethodOutDto>>(methods);

                return Ok(mappedMethods);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}