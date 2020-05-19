using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestController : ControllerBase
    {
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<PaymentRequestController> _logger;
        private readonly IMapper _mapper;

        public PaymentRequestController(IPaymentRequestService paymentRequestService, ILogger<PaymentRequestController> logger, IMapper mapper, ICurrentUser currentUser)
        {
            _paymentRequestService = paymentRequestService;
            _currentUser = currentUser;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create/{receiver}")]
        public async Task<IActionResult> CreatePaymentRequest(string receiver, DeliveryAddressDto deliveryAddressDto)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                var deliveryAddress = _mapper.Map<DeliveryAddress>(deliveryAddressDto);

                await _paymentRequestService.CreatePaymentRequest(currentUserId, receiver, deliveryAddress);

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