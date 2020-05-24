using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserManager _userManager;
        private readonly IUserHelper _userHelper;

        public UserService(IUserManager userManager, IUserHelper userHelper)
        {
            _userManager = userManager;
            _userHelper = userHelper;
        }

        public async Task DeleteUser(string currentUserId, string userId)
        {
            var userToBeDeleted = await _userHelper.FindUserByIdAsync(userId);

            if (await CurrentUserHasPermissionToManageAnotherUsersAccount(currentUserId, userToBeDeleted))
            {
                await _userManager.DeleteAsync(userToBeDeleted);
            }
            else
            {
                throw new ObjectDeleteException(ExceptionStrings.Permission);
            }
        }

        public async Task PromoteToRole(string currentUserId, string userId, string role)
        {
            var userToBePromoted = await _userHelper.FindUserByIdAsync(userId);

            if (await CurrentUserHasPermissionToManageAnotherUsersAccount(currentUserId, userToBePromoted, role))
            {
                await _userManager.AddUserToRoleAsync(userToBePromoted, role);
            }
            else
            {
                throw new ObjectException(ExceptionStrings.Permission);
            }
        } 
        
        public async Task DemoteToRole(string currentUserId, string userId, string role)
        {
            var userToBeDemoted = await _userHelper.FindUserByIdAsync(userId);

            if (await CurrentUserHasPermissionToManageAnotherUsersAccount(currentUserId, userToBeDemoted, role))
            {
                await _userManager.RemoveUserFromRoleAsync(userToBeDemoted, role);
            }
            else
            {
                throw new ObjectException(ExceptionStrings.Permission);
            }
        }

        public async Task<(int Total, IEnumerable<User> Users)> GetUsersInRole(string role, PaginationQuery paginationQuery)
        {
            RoleConstants.RoleExists(role);

            var users = await _userManager.GetUsersInRoleAsync(role.ToLower());

            return (users.Count(), users.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                                        .Take(paginationQuery.PageSize));
        }

        private async Task<bool> CurrentUserHasPermissionToManageAnotherUsersAccount(string currentUserId, User anotherUser, string role = null)
        {
            if (role != null)
                RoleConstants.RoleExists(role);

            var anotherUserMainRole = RoleConstants.GetMainRole(await _userManager.GetUserRolesAsync(anotherUser));

            var currentUser = await _userManager.FindByIdAsync(currentUserId);
            var currentUserMainRole = RoleConstants.GetMainRole(await _userManager.GetUserRolesAsync(currentUser));

            return RoleConstants.GetRolePriority(currentUserMainRole) > RoleConstants.GetRolePriority(anotherUserMainRole) 
                   && (!role?.Equals(currentUserMainRole) ?? true);
        }
    }
}
