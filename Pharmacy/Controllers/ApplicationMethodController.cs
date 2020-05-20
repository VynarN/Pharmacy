using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.Out;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using System;
using System.Collections.Generic;
using System.Net;
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

        public ApplicationMethodController(ILogger<ApplicationMethodController> logger, IApplicationMethodService applicationMethodService, ICategoryService categoryService, IMapper mapper)
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
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
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
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }
    }
}