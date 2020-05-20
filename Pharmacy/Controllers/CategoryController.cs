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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin,mainadmin")]
        [HttpPost("create/{categoryName}")]
        public async Task<IActionResult> CreateCategory(string categoryName)
        {
            try
            {
                await _categoryService.CreateCategory(categoryName);

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
        public IActionResult GetCategories()
        {
            try
            {
                var categories =  _categoryService.GetCategories();

                var mappedCategories = _mapper.Map<IEnumerable<CategoryOutDto>>(categories);

                return Ok(mappedCategories);
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