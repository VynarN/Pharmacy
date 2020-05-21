using Pharmacy.Domain.Entites;
using System.Collections.Generic;

namespace Pharmacy.Domain.Common.ValueObjects
{
    public class GroupedOutcomingPaymentRequest
    {
        public IEnumerable<PaymentRequest> PaymentRequests { get; set; }

        public string ReceiverEmail { get; set; }

        public string Status { get; set; }

        public string Created { get; set; }
    }
}
