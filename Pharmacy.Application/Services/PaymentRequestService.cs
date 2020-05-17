using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IRepository<PaymentRequest> _repository;

        public PaymentRequestService(IRepository<PaymentRequest> repository)
        {
            _repository = repository;
        }

        public async Task CreatePaymentRequest(PaymentRequest paymentRequest)
        {
            await _repository.Create(paymentRequest);
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
