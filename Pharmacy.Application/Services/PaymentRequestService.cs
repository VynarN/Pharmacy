using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Application.Common.Validators;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IRepository<PaymentRequest> _paymentRequestRepo;
        private readonly IRepository<BasketItem> _basketItemRepo;
        private readonly IDeliveryAddressService _deliveryAddressService;
        private readonly IOrderService _orderService;

        public PaymentRequestService(IRepository<PaymentRequest> paymentRequestRepo, IRepository<BasketItem> basketItemRepo, 
                                     IDeliveryAddressService deliveryAddressService, IOrderService orderService)
        {
            _basketItemRepo = basketItemRepo;
            _paymentRequestRepo = paymentRequestRepo;
            _orderService = orderService;
            _deliveryAddressService = deliveryAddressService;
        }

        public async Task CreatePaymentRequest(string senderId, string receiverEmail, DeliveryAddress deliveryAddress)
        {
            var senderBasketItems = _basketItemRepo.GetWithInclude(bi => bi.UserId.Equals(senderId), bi => bi.Medicament);

            var addressId = deliveryAddress.Id != 0 ? deliveryAddress.Id : await _deliveryAddressService.CreateDeliveryAddress(deliveryAddress);

            var paymentRequests = senderBasketItems.Select(basketItem => new PaymentRequest()
            {
                SenderId = senderId,
                ReceiverEmail = receiverEmail,
                DeliveryAddressId = addressId,
                MedicamentId = basketItem.MedicamentId,
                Quantity = basketItem.ProductQuantity,
                Total = basketItem.Medicament.Price * basketItem.ProductQuantity,
                RequestedAt = DateTime.Now,
                RequestStatus = RequestStatus.Pending
            });

            await _paymentRequestRepo.Create(paymentRequests);

            await _basketItemRepo.Delete(senderBasketItems);
        }

        public async Task AcceptPaymentRequest(string senderId, string receiverEmail, string createdAt)
        {
            var requests = GetRequests(senderId, receiverEmail, createdAt);

            await _orderService.CreateOrderFromPaymentRequest(requests);

            foreach (var request in requests)
            {
                request.RequestStatus = RequestStatus.Accepted;
            }

            await _paymentRequestRepo.Update(requests);
        }

        public async Task DeclinePaymentRequest(string senderId, string receiverEmail, string createdAt)
        {
            var requests = GetRequests(senderId, receiverEmail, createdAt);

            foreach (var request in requests)
            {
                request.RequestStatus = RequestStatus.Declined;
            }

            await _paymentRequestRepo.Update(requests);
        }

        public async Task DeletePaymentRequest(string senderId, string receiverEmail, string createdAt)
        {
            var requests = GetRequests(senderId, receiverEmail, createdAt);

            await _paymentRequestRepo.Delete(requests);
        }

        public IEnumerable<GroupedIncomingPaymentRequest> GetIncoming(out int totalCount, string receiverEmail, PaginationQuery paginationQuery)
        {
            var requests = _paymentRequestRepo.GetWithInclude(pr => pr.ReceiverEmail.Equals(receiverEmail), pr => pr.Sender)
                                               .AsEnumerable()
                                               .GroupBy(obj => new
                                               {
                                                   Sender = obj.Sender.Email,
                                                   Status = obj.RequestStatus,
                                                   Created = obj.RequestedAt.ToString(StringConstants.DateTimeFormat)
                                               });
            totalCount = requests.Count();

            return requests.Select(g => new GroupedIncomingPaymentRequest()
                                   {
                                       SenderEmail = g.Key.Sender,
                                       Status = g.Key.Status.ToString(),
                                       Created = g.Key.Created,
                                       PaymentRequests = g.Select(request => request)
                                   })
                           .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                           .Take(paginationQuery.PageSize);
        }

        public IEnumerable<GroupedOutcomingPaymentRequest> GetOutcoming(out int totalCount, string senderId, PaginationQuery paginationQuery)
        {
            var requests = _paymentRequestRepo.GetWithInclude(pr => pr.SenderId.Equals(senderId), pr => pr.Sender)
                                              .AsEnumerable()
                                              .GroupBy(obj => new
                                              {
                                                  Receiver = obj.ReceiverEmail,
                                                  Status = obj.RequestStatus,
                                                  Created = obj.RequestedAt.ToString(StringConstants.DateTimeFormat)
                                              });

            totalCount = requests.Count();

            return requests.Select(g => new GroupedOutcomingPaymentRequest()
                                   {
                                       ReceiverEmail = g.Key.Receiver,
                                       Status = g.Key.Status.ToString(),
                                       Created = g.Key.Created,
                                       PaymentRequests = g.Select(request => request)
                                   
                                   })
                           .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                           .Take(paginationQuery.PageSize);
        }

        private IEnumerable<PaymentRequest> GetRequests(string senderId, string receiverEmail, string createdAt)
        {
            try
            {
                StringArgumentValidator.ValidateStringArgument(createdAt, nameof(createdAt));

                DateTime.Parse(createdAt);
            }
            catch (Exception)
            {
                throw new ArgumentException(ModelValidationStrings.DateTime, nameof(createdAt));
            }

            return _paymentRequestRepo.GetByPredicate(pr => pr.SenderId.Equals(senderId) &&
                                                            pr.ReceiverEmail.Equals(receiverEmail))
                                      .AsEnumerable()
                                      .Where(pr => pr.RequestedAt.ToString(StringConstants.DateTimeFormat).Equals(createdAt));
        }
    }
}
