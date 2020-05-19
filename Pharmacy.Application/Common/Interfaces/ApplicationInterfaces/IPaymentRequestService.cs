﻿using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IPaymentRequestService
    {
        Task CreatePaymentRequest(string userId, string receiverEmail, DeliveryAddress deliveryAddress);

        Task DeletePaymentRequest(int paymentRequestId);

        IQueryable<PaymentRequest> GetPaymentRequests(PaginationQuery paginationQuery);
    }
}
