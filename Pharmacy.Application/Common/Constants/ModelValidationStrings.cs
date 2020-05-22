namespace Pharmacy.Application.Common.Constants
{
    public static class ModelValidationStrings
    {
        public const string ArabicNumerals = "Password must include at least one Arabic numerals. ";
        public const string LatinLetter = "Password must include at least one latin letter. ";
        public const string UpperCase = "Password must include at least one uppercase letter. ";
        public const string LowerCase = "Password must include at least one lowercase letter. ";
        public const string PasswordLength = "Password length must be at least 8 and no more than 100 characters. ";
        public const string ConfirmPassword = "Passwords do not match. ";
        public const string EmailAddress = "Invalid email address. ";
        public const string EmailAddressUniqueness = "Specified email address is already taken. ";
        public const string NegativeIntegerValue = "Invalid input data. Negative value is not acceptable. ";
        public const string ValueOutOfRange = "Value is out of range. ";
        public const string PhoneNumber = "Invalid phone number. ";
        public const string PhoneNumberUniqueness = "Specified phone number is already taken. ";
        public const string EmptyField = "Field cannot be empty. ";
        public const string DateTime = "Invalid date and time. ";
        public const string ProductQuantity = "Requested number of {0} cannot be ordered. Number remaining in stock: {1}. ";
    }
}
