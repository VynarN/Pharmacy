using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.DTO;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using System;
using System.Collections.Generic;

namespace Pharmacy.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {
        private readonly IDeliveryAddressService _deliveryAddressService;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<DeliveryAddressController> _logger;
        private readonly IMapper _mapper;

        public DeliveryAddressController(IDeliveryAddressService deliveryAddressService, ICurrentUser currentUser,
                                         ILogger<DeliveryAddressController> logger, IMapper mapper)
        {
            _deliveryAddressService = deliveryAddressService;
            _logger = logger;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        
        [HttpGet("get")]
        public IActionResult GetDeliveryAddresses()
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                var deliveryAddresses = _deliveryAddressService.GetDeliveryAddresses(currentUserId);

                var  mappedAddresses = _mapper.Map<IEnumerable<DeliveryAddressDto>>(deliveryAddresses);

                return Ok(mappedAddresses);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}