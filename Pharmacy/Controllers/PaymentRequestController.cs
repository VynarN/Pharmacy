using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
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

        [HttpPost("create/{receiverEmail}")]
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
            catch (ObjectException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get/incoming")]
        public async Task<IActionResult> GetIncomingPaymentRequests([FromQuery]PaginationQuery paginationQuery)
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
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpGet("get/outcoming")]
        public IActionResult GetOutcomingPaymentRequests([FromQuery]PaginationQuery paginationQuery)
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
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpPut("accept")]
        public async Task<IActionResult> AcceptPaymentRequest(string senderEmail, string createdAt)
        {
            try
            {
                var sender = await _userHelper.FindUserByEmailAsync(senderEmail);

                var receiver = await _userHelper.FindUserByIdAsync(_currentUser.UserId);

                if (sender.Id.Equals(_currentUser.UserId))
                    return Forbid();

                await _paymentRequestService.AcceptPaymentRequest(sender.Id, receiver.Email, createdAt);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ObjectException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }

        [HttpPut("decline")]
        public async Task<IActionResult> DeclinePaymentRequest(string senderEmail, string createdAt)
        {
            try
            {
                var sender = await _userHelper.FindUserByEmailAsync(senderEmail);

                var receiver = await _userHelper.FindUserByIdAsync(_currentUser.UserId);

                if (sender.Id.Equals(_currentUser.UserId))
                    return Forbid();

                await _paymentRequestService.DeclinePaymentRequest(sender.Id, receiver.Email, createdAt);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ObjectException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
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
            catch (ObjectException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}