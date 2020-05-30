namespace Pharmacy.Application.Common.Constants
{
    public static class ExceptionStrings
    {
        public const string ArgumentExceptionPlaceholder = "Invalid argument: {0}. ";
        public const string InvalidArgumentString = "String argument can not be null or empty. ";
        public const string NullArgumentException = "Invalid input data. Argument cannot be null or empty. ";
        public const string ObjectNotFoundException = "Invalid search data. Object is not found. ";
        public const string RoleNotFoundException = "Specified role could not be found. ";
        public const string AddToRoleException = "Failed to add role to user. ";
        public const string EmailConfirmException = "Failed to confirm email. Invalid confirmation token could have caused this failure. ";
        public const string ResetPasswordException = "Failed to reset password. Invalid confirmation token could have caused this failure. ";
        public const string UserNotFoundException = "User with specified data is not found. ";
        public const string LoginException = "Cannot find user with specified email and password. ";
        public const string AccessTokenException = "Invalid access token. ";
        public const string RefreshTokenException = "Invalid refresh token. ";
        public const string SendEmailException = "Failed to send an email. ";
        public const string FileUploading = "An error occured while uploading a file. ";
        public const string Exception = "Oops... Something went wrong. Please contact support.";
        public const string Permission = "You do not have any permission to manage specified user's account. ";
        public const string EmptyPaymentRequests = "List of payment requests is empty. ";
        public const string EmptyBasketItems = "Basket is empty. ";
    }
}
