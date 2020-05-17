using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.BasketItemIn;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Domain.Entites;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IBasketItemService _basketItemService;
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IMapper _mapper;

        public BasketItemController(IBasketItemService basketItemService, ILogger<ManufacturerController> logger, IMapper mapper)
        {
            _basketItemService = basketItemService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddBasketItem(BasketItemInDto basketItemDto)
        {
            try
            {
                var basketItem = _mapper.Map<BasketItem>(basketItemDto);

                await _basketItemService.CreateBasketItem(basketItem);

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