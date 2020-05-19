using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO;
using Pharmacy.Application.Common.DTO.Out.OrderOut;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger, IMapper mapper, ICurrentUser currentUser)
        {
            _orderService = orderService;
            _currentUser = currentUser;
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

                await _orderService.CreateOrders(currentUserId, deliveryAddress);

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
        public IActionResult GetOrders([FromQuery]PaginationQuery paginationQuery)
        {
            try
            {
                var groupedOrders = _orderService.GetOrders(paginationQuery);

                var formatedOrders = _mapper.Map<IEnumerable<GroupedOrdersDto>>(groupedOrders);

                return Ok(formatedOrders);
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