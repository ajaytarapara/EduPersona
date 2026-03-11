namespace EduPersona.Assesment.Shared.Constants
{
    public class ValidationMessages
    {

    }

    public class ErrorMessage
    {
        public static readonly Func<string, string> NotFoundMessage = (entityName) => $"{entityName} not found.";
        public static string AccessTokenGenerateError => "Your session is invalid.";
        public const string RefreshTokenExpired = "Refresh token expired.";
        public const string InvalidAccessToken = "Access token invalid.";
        public const string AccessTokenExpired = "Access token expired.";
        public const string RefreshTokenInvalid = "Refresh token invalid.";
    }

    public class SuccessMessage
    {
        public const string RequestSuccessful = "Request successful.";

        public const string LoginSuccessfully = "Login successfully.";
        public static readonly Func<string, string> SuccessfullyMessage = (entityName) => $"{entityName} successfully.";
    }
}