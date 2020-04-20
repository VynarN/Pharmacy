namespace Pharmacy.Application.Common.Constants
{
    public static class ModelValidationStrings
    {
        public const string ArabicNumerals = "Password must include at least one Arabic numerals. ";
        public const string LatinLetter = "Password must include at least one latin letter. ";
        public const string UpperCase = "Password must include at least one uppercase letter. ";
        public const string LowerCase = "Password must include at least one lowercase letter. ";
        public const string Alphanumeric = "Password must not include any non alphanumeric characters! ";
        public const string PasswordLength = "Password length must be at least 8 characters. ";
        public const string ConfirmPassword = "Passwords do not match. ";
        public const string EmailAddress = "Invalid email address. ";
        public const string NegativeIntegerValue = "Invalid input data. Negative value is not acceptable. ";
        public const string ValueOutOfRange = "Value is out of range. ";
    }
}
