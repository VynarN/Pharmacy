using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IPaymentRequestService
    {
        /// <summary>
        /// Creates payment request for items in a sender's basket.
        /// </summary>
        /// <param name="senderId"> Id of a user who has sent request.</param>
        /// <param name="receiverEmail"> Email of a user to whom the request has been sent.</param>
        /// <param name="deliveryAddress"> Address where items specified in the payment request will be send to.</param>
        Task CreatePaymentRequest(string senderId, string receiverEmail, DeliveryAddress deliveryAddress);

        /// <summary>
        /// Deletes payment request.
        /// </summary>
        /// <param name="senderId"> Id of a user who has sent request.</param>
        /// <param name="receiverEmail"> Email of a user to whom the request has been sent.</param>
        /// <param name="createdAt"> Date and time when request has been created.</param>
        Task DeletePaymentRequest(string senderId, string receiverEmail, string createdAt);

        /// <summary>
        /// Updates payment request status to RequestStatus.Accepted and creates order for user who has sent request.
        /// </summary>
        /// <param name="senderId"> Id of a user who has sent request.</param>
        /// <param name="receiverEmail"> Email of a user to whom the request has been sent.</param>
        /// <param name="createdAt"> Date and time when request has been created.</param>
        Task AcceptPaymentRequest(string senderId, string receiverEmail, string  createdAt);

        /// <summary>
        /// Updates payment request status to RequestStatus.Declined.
        /// </summary>
        /// <param name="senderId"> Id of a user who has sent request.</param>
        /// <param name="receiverEmail"> Email of a user to whom the request has been sent.</param>
        /// <param name="createdAt"> Date and time when request has been created.</param>
        Task DeclinePaymentRequest(string senderId, string receiverEmail, string createdAt);

        /// <summary>
        /// Get payment requests received by a user.
        /// </summary>
        /// <param name="totalCount"> Total number of incoming payment requests</param>
        /// <param name="receiverEmail"> Email of a user to whom the request has been sent.</param>
        /// <param name="paginationQuery"> Defines pagination parameters such as page number and page size.</param>
        /// <returns>
        /// Returns a list of incoming payment requests that are grouped by request's sender email, status and date and time they have been created.
        /// </returns>
        IEnumerable<GroupedIncomingPaymentRequest> GetIncoming(out int totalCount, string receiverEmail, PaginationQuery paginationQuery);

        /// <summary>
        /// Get payment requests sent by a user.
        /// </summary>
        /// <param name="totalCount"> Total number of incoming payment requests</param>
        /// <param name="receiverEmail"> Email of a user to whom the request has been sent.</param>
        /// <param name="paginationQuery"> Defines pagination parameters such as page number and page size.</param>
        /// <returns>
        /// Returns a list of outcoming payment requests that are grouped by request's receiver email, status and date and time they have been created.
        /// </returns>
        IEnumerable<GroupedOutcomingPaymentRequest> GetOutcoming(out int totalCount, string senderId, PaginationQuery paginationQuery);
    }
}
