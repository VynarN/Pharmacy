using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IPaymentRequestService _paymentRequestService;

        public PaymentRequestService(IPaymentRequestService paymentRequestService)
        {
            _paymentRequestService = paymentRequestService;
        }

        public async Task CreatePaymentRequest(PaymentRequest paymentRequest)
        {
            await _paymentRequestService.CreatePaymentRequest(paymentRequest);
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
