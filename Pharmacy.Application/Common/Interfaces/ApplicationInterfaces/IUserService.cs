using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IUserService
    {
        Task<(int Total, IEnumerable<User> Users)> GetUsersInRole(string role, PaginationQuery paginationQuery);

        Task DeleteUser(string currentUserId, string userId);

        Task PromoteToRole(string currentUserId, string userId, string role);

        Task DemoteToRole(string currentUserId, string userId, string role);
    }
}
