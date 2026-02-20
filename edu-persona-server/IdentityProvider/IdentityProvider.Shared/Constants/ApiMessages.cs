namespace IdentityProvider.Shared.Constants
{
    public class ApiMessages
    {
        public const string UserRegisteredSuccessfully = "User registered successfully.";
        public const string UserAlreadyExists = "User already exists.";
        public static readonly Func<string, string> NotFoundMessage = (entityName) => $"{entityName} not found.";
        public static readonly Func<string, string> InvalidMessage = (entityName) => $"Invalid {entityName}.";
        public const string LoginSuccessfully = "Login successfully.";
        public const string RefreshTokenExpired = "Refresh token expired.";
        public const string SessionExpired = "Session expired.";
        public const string RequestSuccessful = "Request successful.";
        public static readonly Func<string, string> SuccessfullyMessage = (entityName) => $"{entityName} successfully.";
        public const string LogoutFail = "Logout failed. Please try again.";

    }
}
