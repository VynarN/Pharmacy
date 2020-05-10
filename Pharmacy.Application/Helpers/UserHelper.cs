using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Validators;
using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Helpers
{
    public class UserHelper: IUserHelper
    {
        private readonly IUserManager _userManager;

        public UserHelper(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            StringArgumentValidator.ValidateStringArgument(id, nameof(id));

            var user = await _userManager.FindByIdAsync(id.ToUpper());

            if (user == null)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, id);

            return user;
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            StringArgumentValidator.ValidateStringArgument(email, nameof(email));

            var user = await _userManager.FindByEmailAsync(email.ToUpper());

            if (user == null)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, email);

            return user;
        }
    }
}
