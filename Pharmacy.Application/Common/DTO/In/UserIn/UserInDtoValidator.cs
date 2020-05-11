using FluentValidation;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.DTO.In.UserIn
{
    public class UserInDtoValidator: AbstractValidator<UserInDto>
    {
        private readonly IUserManager _userManager;

        public UserInDtoValidator(IUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(obj => obj.Email).NotEmpty().EmailAddress().WithMessage(ModelValidationStrings.EmailAddress)
                .MustAsync(CheckEmailUniqueness).WithMessage(ModelValidationStrings.EmailAddressUniqueness);

            RuleFor(obj => obj.FirstName).NotEmpty().WithMessage(ModelValidationStrings.PhoneNumber);

            RuleFor(obj => obj.SecondName).NotEmpty().WithMessage(ModelValidationStrings.PhoneNumber);

            RuleFor(obj => obj.PhoneNumber).NotEmpty().Length(10).WithMessage(ModelValidationStrings.PhoneNumber);

            RuleFor(obj => obj.PhoneNumber).Must(phone => phone.FirstOrDefault(ch => char.IsLetter(ch)) == '\0')
                .WithMessage(ModelValidationStrings.PhoneNumber);
        }

        public async Task<bool> CheckEmailUniqueness(string email, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(email) == null;
        }
    }
}
