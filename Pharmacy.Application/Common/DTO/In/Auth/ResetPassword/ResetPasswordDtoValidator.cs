using FluentValidation;
using Pharmacy.Application.Common.Constants;
using System.Linq;

namespace Pharmacy.Application.Common.DTO.In.Auth.ResetPassword
{
    public class ResetPasswordDtoValidator: AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(obj => obj.Password).NotEmpty().MinimumLength(8).WithMessage(ModelValidationStrings.PasswordLength);
            RuleFor(obj => obj.Password).MaximumLength(100).WithMessage(ModelValidationStrings.PasswordLength);

            RuleFor(obj => obj.Password).Must(pass => pass.FirstOrDefault(ch => char.IsLetter(ch)) != '\0')
                .WithMessage(ModelValidationStrings.LatinLetter);

            RuleFor(obj => obj.Password).Must(pass => pass.FirstOrDefault(ch => char.IsDigit(ch)) != '\0')
                .WithMessage(ModelValidationStrings.ArabicNumerals);

            RuleFor(obj => obj.Password).Must(pass => pass.FirstOrDefault(ch => char.IsLower(ch)) != '\0')
                .WithMessage(ModelValidationStrings.LowerCase);

            RuleFor(obj => obj.Password).Must(pass => pass.FirstOrDefault(ch => char.IsUpper(ch)) != '\0')
                .WithMessage(ModelValidationStrings.UpperCase);

            RuleFor(obj => obj.ConfirmPassword).Equal(obj => obj.Password).WithMessage(ModelValidationStrings.ConfirmPassword);
        }
    }
}
