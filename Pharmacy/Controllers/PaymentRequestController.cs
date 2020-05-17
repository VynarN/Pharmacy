using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In.PaymentRequestIn;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
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
        private readonly ILogger<PaymentRequestController> _logger;
        private readonly IMapper _mapper;

        public PaymentRequestController(IPaymentRequestService paymentRequestService, ILogger<PaymentRequestController> logger, IMapper mapper)
        {
            _paymentRequestService = paymentRequestService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddBasketItem(PaymentRequestInDto paymentRequestDto)
        {
            try
            {
                var basketItem = _mapper.Map<PaymentRequest>(paymentRequestDto);

                await _paymentRequestService.CreatePaymentRequest(basketItem);

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