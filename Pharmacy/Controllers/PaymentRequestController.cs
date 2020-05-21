using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO;
using Pharmacy.Application.Common.DTO.Out.PaymentRequestOut;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRequestController : ControllerBase
    {
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly ICurrentUser _currentUser;
        private readonly IPaginationService _paginationService;
        private readonly IUserHelper _userHelper;
        private readonly ILogger<PaymentRequestController> _logger;
        private readonly IMapper _mapper;

        public PaymentRequestController(IPaymentRequestService paymentRequestService, ILogger<PaymentRequestController> logger,
                                        IMapper mapper, ICurrentUser currentUser, 
                                        IUserHelper userHelper, IPaginationService paginationService)
        {
            _paymentRequestService = paymentRequestService;
            _paginationService = paginationService;
            _currentUser = currentUser;
            _userHelper = userHelper;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create/{receiver}")]
        public async Task<IActionResult> CreatePaymentRequest(string receiverEmail, DeliveryAddressDto deliveryAddressDto)
        {
            try
            {
                var currentUserId = _currentUser.UserId;

                var deliveryAddress = _mapper.Map<DeliveryAddress>(deliveryAddressDto);

                var receiver = await _userHelper.FindUserByEmailAsync(receiverEmail);

                await _paymentRequestService.CreatePaymentRequest(currentUserId, receiver.Email, deliveryAddress);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }

        [HttpGet("get/incoming")]
        public async Task<IActionResult> GetIncomingPaymentRequests(PaginationQuery paginationQuery)
        {
            try
            {
                var receiverEmail = (await _userHelper.FindUserByIdAsync(_currentUser.UserId)).Email;

                var incomingRequests = _paymentRequestService.GetIncoming(out int requestsTotal, receiverEmail, paginationQuery);

                var mappedRequests = _mapper.Map<IEnumerable<GroupedInPaymentRequestsDto>>(incomingRequests);

                var paginatedResponse = _paginationService.FormPaginatedResponse(requestsTotal, mappedRequests, paginationQuery);

                return Ok(paginatedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }

        [HttpGet("get/outcoming")]
        public IActionResult GetOutcomingPaymentRequests(PaginationQuery paginationQuery)
        {
            try
            {
                var senderId = _currentUser.UserId;

                var outcomingRequests = _paymentRequestService.GetOutcoming(out int requestsTotal, senderId, paginationQuery);

                var mappedRequests = _mapper.Map<IEnumerable<GroupedOutPaymentRequestDto>>(outcomingRequests);

                var paginatedResponse = _paginationService.FormPaginatedResponse(requestsTotal, mappedRequests, paginationQuery);

                return Ok(paginatedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }

        [HttpPut("accept")]
        public async Task<IActionResult> AcceptPaymentRequest(string senderEmail, string createdAt)
        {
            try
            {
                var sender = await _userHelper.FindUserByEmailAsync(senderEmail);

                var receiver = await _userHelper.FindUserByIdAsync(_currentUser.UserId);

                await _paymentRequestService.AcceptPaymentRequest(sender.Id, receiver.Email, createdAt);

                return NoContent();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }

        [HttpPut("decline")]
        public async Task<IActionResult> DeclinePaymentRequest(string senderEmail, string createdAt)
        {
            try
            {
                var sender = await _userHelper.FindUserByEmailAsync(senderEmail);

                var receiver = await _userHelper.FindUserByIdAsync(_currentUser.UserId);

                await _paymentRequestService.DeclinePaymentRequest(sender.Id, receiver.Email, createdAt);

                return NoContent();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(ExceptionStrings.Exception);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePaymentRequest(string receiverEmail, string createdAt)
        {
            try
            {
                var senderId = _currentUser.UserId;

                var receiver = (await _userHelper.FindUserByEmailAsync(receiverEmail));

                await _paymentRequestService.DeletePaymentRequest(senderId, receiver.Email, createdAt);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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