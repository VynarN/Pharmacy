using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
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

        public PaymentRequestService(IRepository<PaymentRequest> paymentRequestRepo, IRepository<BasketItem> basketItemRepo, IDeliveryAddressService deliveryAddressService)
        {
            _basketItemRepo = basketItemRepo;
            _paymentRequestRepo = paymentRequestRepo;
            _deliveryAddressService = deliveryAddressService;
        }

        public async Task CreatePaymentRequest(string userId, string receiverEmail, DeliveryAddress deliveryAddress)
        { 
            var userBasketItems = _basketItemRepo.GetWithInclude(bi => bi.UserId.Equals(userId), bi => bi.Medicament);

            var addressId = deliveryAddress.Id != 0 ? deliveryAddress.Id : await _deliveryAddressService.CreateDeliveryAddress(deliveryAddress);

            var paymentRequests = userBasketItems.Select(basketItem => new PaymentRequest()
            {
                SenderId = userId,
                ReceiverEmail = receiverEmail,
                DeliveryAddressId = addressId,
                MedicamentId = basketItem.MedicamentId,
                Quantity = basketItem.ProductQuantity,
                Total = basketItem.Medicament.Price * basketItem.ProductQuantity,
                RequestedAt = DateTime.Now,
                
            });

            await _paymentRequestRepo.Create(paymentRequests);

            await _basketItemRepo.Delete(userBasketItems);
        }

        public Task DeletePaymentRequest(int paymentRequestId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<PaymentRequest> GetPaymentRequests(PaginationQuery paginationQuery)
        {
            throw new System.NotImplementedException();
        }
    }
}
