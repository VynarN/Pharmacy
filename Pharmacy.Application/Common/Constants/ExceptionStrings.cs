namespace Pharmacy.Application.Common.Constants
{
    public static class ExceptionStrings
    {
        public const string ArgumentExceptionPlaceholder = "Invalid argument: {0}. ";
        public const string InvalidArgumentString = "String argument can not be null or empty. ";
        public const string NullArgumentException = "Invalid input data. Argument cannot be null or empty. ";
        public const string ObjectNotFoundException = "Invalid search data. Object is not found. ";
        public const string ObjectUpdateException = "Invalid object data. Failed to update an object. ";
        public const string ObjectCreateException = "Invalid object data. Failed to create an object. ";
        public const string ObjectDeleteException = "Failed to delete an object. ";
        public const string RolePromoteException = "Failed to promote user. ";
        public const string RoleDemoteException = "Failed to demote user. ";
        public const string RoleNotFoundException = "Specified role does not exist. ";
        public const string AddToRoleException = "Failed to add role to user. ";
        public const string EmailConfirmException = "Failed to confirm email. Invalid confirmation token could have caused this failure. ";
        public const string ResetPasswordException = "Failed to reset password. Invalid confirmation token could have caused this failure. ";
        public const string UserNotFoundException = "User with specified data is not found. ";
        public const string UserUpdateException = "Invalid user data. Failed to update user. ";
        public const string LoginException = "Cannot find user with specified email and password. ";
        public const string CreateUserException = "Cannot create an account. Specified email address and/or phone number already belongs to someone. ";
        public const string AccessTokenException = "Invalid access token. ";
        public const string RefreshTokenException = "Invalid refresh token. ";
        public const string DeleteUserException = "Failed to delete a user. ";
        public static string SendEmailException = "Failed to send an email. ";
        public static string Exception = "Oops... Something went wrong. Please contact support.";
    }
}
