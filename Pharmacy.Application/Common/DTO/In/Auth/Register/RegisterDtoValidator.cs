using FluentValidation;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.DTO.In.Auth.Register
{
    public class RegisterDtoValidator:  AbstractValidator<RegisterDto>
    {
        private readonly IUserManager _userManager;

        public RegisterDtoValidator(IUserManager userManager)
        {
            _userManager = userManager;

            Validate();
        }

        private void Validate()
        {
            RuleFor(obj => obj.Email).NotEmpty().EmailAddress().WithMessage(ModelValidationStrings.EmailAddress)
                .MustAsync(CheckEmailUniqueness).WithMessage(ModelValidationStrings.EmailAddressUniqueness);

            RuleFor(obj => obj.FirstName).NotEmpty().MaximumLength(100);

            RuleFor(obj => obj.SecondName).NotEmpty().MaximumLength(100);

            RuleFor(obj => obj.PhoneNumber).NotEmpty().Length(10).WithMessage(ModelValidationStrings.PhoneNumber)
                .Must(phone => phone.FirstOrDefault(ch => char.IsLetter(ch)) == '\0').WithMessage(ModelValidationStrings.PhoneNumber);

            RuleFor(obj => obj.Password).NotEmpty().MinimumLength(8).WithMessage(ModelValidationStrings.PasswordLength)
                                                   .MaximumLength(100).WithMessage(ModelValidationStrings.PasswordLength);

            RuleFor(obj => obj.Password).Must(pass => pass.FirstOrDefault(ch => char.IsLetter(ch)) != '\0')
                .WithMessage(ModelValidationStrings.LatinLetter)
                .Must(pass => pass.FirstOrDefault(ch => char.IsDigit(ch)) != '\0')
                .WithMessage(ModelValidationStrings.ArabicNumerals)
                .Must(pass => pass.FirstOrDefault(ch => char.IsLower(ch)) != '\0')
                .WithMessage(ModelValidationStrings.LowerCase)
                .Must(pass => pass.FirstOrDefault(ch => char.IsUpper(ch)) != '\0')
                .WithMessage(ModelValidationStrings.UpperCase);

            RuleFor(obj => obj.ConfirmPassword).Equal(obj => obj.Password).WithMessage(ModelValidationStrings.ConfirmPassword);
        }

        public async Task<bool> CheckEmailUniqueness(string email, CancellationToken cancellationToken)
        {
            return await _userManager.FindByEmailAsync(email) == null;
        }
    }
}
