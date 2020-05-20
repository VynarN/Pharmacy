using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.BasketItemIn;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemController : ControllerBase
    {
        private readonly IBasketItemService _basketItemService;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<BasketItemController> _logger;
        private readonly IMapper _mapper;

        public BasketItemController(IBasketItemService basketItemService, ILogger<BasketItemController> logger, 
                                    IMapper mapper, ICurrentUser currentUser)
        {
            _basketItemService = basketItemService;
            _logger = logger;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBasketItem(BasketItemInDto basketItemDto)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                var basketItem = _mapper.Map<BasketItem>(basketItemDto);
                basketItem.UserId = currentUserId;

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