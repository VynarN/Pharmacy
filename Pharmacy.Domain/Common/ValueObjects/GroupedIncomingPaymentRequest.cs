using Pharmacy.Domain.Entites;
using System.Collections.Generic;

namespace Pharmacy.Domain.Common.ValueObjects
{
    public class GroupedIncomingPaymentRequest
    {
        public IEnumerable<PaymentRequest> PaymentRequests { get; set; }

        public string SenderEmail { get; set; }

        public string Status { get; set; }

        public string Created { get; set; }
    }
}
