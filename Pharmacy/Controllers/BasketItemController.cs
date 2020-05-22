using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.BasketItemIn;
using Pharmacy.Application.Common.DTO.Out;
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
                var basketItem = MapDtoToBasketItem(basketItemDto);

                await _basketItemService.CreateBasketItem(basketItem);

                return Ok();
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get")]
        public IActionResult GetUserBasketItems()
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                var userBasketItems = _basketItemService.GetBasketItems(currentUserId);

                var mappedBasketItems = userBasketItems.ProjectTo<BasketItemOutDto>(_mapper.ConfigurationProvider);

                return Ok(mappedBasketItems);
            }
            catch(Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBasketItem(BasketItemInDto basketItemDto)
        {
            try
            {
                var basketItem = MapDtoToBasketItem(basketItemDto);

                await _basketItemService.UpdateBasketItem(basketItem);

                return Ok();
            }
            catch(Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBasketItem(BasketItemInDto basketItemDto)
        {
            try
            {
                var basketItem = MapDtoToBasketItem(basketItemDto);

                await _basketItemService.DeleteBasketItem(basketItem);

                return Ok();
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        private BasketItem MapDtoToBasketItem(BasketItemInDto basketItemDto)
        {
            var currentUserId = _currentUser.UserId;

            var basketItem = _mapper.Map<BasketItem>(basketItemDto);

            basketItem.UserId = currentUserId;

            return basketItem;
        }
    }
}