using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaginationService _paginationService;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IPaginationService paginationService,
                               ILogger<OrderController> logger, IMapper mapper, ICurrentUser currentUser)
        {
            _orderService = orderService;
            _currentUser = currentUser;
            _paginationService = paginationService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder(DeliveryAddressDto deliveryAddressDto)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                var deliveryAddress = _mapper.Map<DeliveryAddress>(deliveryAddressDto);

                await _orderService.CreateOrder(currentUserId, deliveryAddress);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }

        [Authorize(Roles = "manager,admin,mainadmin")]
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateOrder(string userId, string createdAt, string orderStatus)
        {
            try
            {
                await _orderService.UpdateOrder(userId, createdAt, orderStatus);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }

        [Authorize(Roles = "manager,admin,mainadmin")]
        [HttpGet("get/{userId}")]
        public IActionResult GetUserOrders([FromQuery]PaginationQuery paginationQuery, string userId)
        {
            try
            {
                var groupedUserOrders = _orderService.GetUserOrders(out int totalOrdersCount, paginationQuery, userId);

                var formatedOrders = _mapper.Map<IEnumerable<GroupedOrdersDto>>(groupedUserOrders);

                var paginatedResponse = _paginationService.FormPaginatedResponse(totalOrdersCount, formatedOrders, paginationQuery);

                return Ok(paginatedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }
        
        [Authorize]
        [HttpGet("get")]
        public IActionResult GetCurrentUserOrders([FromQuery]PaginationQuery paginationQuery)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                var groupedUserOrders = _orderService.GetUserOrders(out int totalOrdersCount, paginationQuery, currentUserId);

                var formatedOrders = _mapper.Map<IEnumerable<GroupedOrdersDto>>(groupedUserOrders);

                var paginatedResponse = _paginationService.FormPaginatedResponse(totalOrdersCount, formatedOrders, paginationQuery);

                return Ok(paginatedResponse);
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