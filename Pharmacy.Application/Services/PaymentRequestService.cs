using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Entites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IRepository<PaymentRequest> _paymentRequestRepo;
        private readonly IRepository<BasketItem> _basketItemRepo;
        private readonly IDeliveryAddressService _deliveryAddressService;
        private readonly IUserHelper _userHelper;

        public PaymentRequestService(IRepository<PaymentRequest> paymentRequestRepo, IRepository<BasketItem> basketItemRepo, 
                                     IDeliveryAddressService deliveryAddressService, IUserHelper userHelper)
        {
            _basketItemRepo = basketItemRepo;
            _paymentRequestRepo = paymentRequestRepo;
            _deliveryAddressService = deliveryAddressService;
            _userHelper = userHelper;
        }

        public async Task CreatePaymentRequest(string senderId, string receiverEmail, DeliveryAddress deliveryAddress)
        {
            var receiver = await _userHelper.FindUserByEmailAsync(receiverEmail);

            var senderBasketItems = _basketItemRepo.GetWithInclude(bi => bi.UserId.Equals(senderId), bi => bi.Medicament);

            var addressId = deliveryAddress.Id != 0 ? deliveryAddress.Id : await _deliveryAddressService.CreateDeliveryAddress(deliveryAddress);

            var paymentRequests = senderBasketItems.Select(basketItem => new PaymentRequest()
            {
                SenderId = senderId,
                ReceiverEmail = receiver.Email,
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

        public Task DeletePaymentRequest(int paymentRequestId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<PaymentRequest> GetIncoming(out int totalCount, PaginationQuery paginationQuery)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PaymentRequest> GetOutcoming(out int totalCount, PaginationQuery paginationQuery)
        {
            throw new NotImplementedException();
        }
    }
}
