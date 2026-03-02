namespace EduPersona.Assesment.Shared.Constants
{
    public class ValidationMessages
    {

    }

    public class ErrorMessage
    {
        public static readonly Func<string, string> NotFoundMessage = (entityName) => $"{entityName} not found.";
        public static string AccessTokenGenerateError => "Your session has expired or invalid. Please try again.";
        public const string RefreshTokenExpired = "Refresh token expired.";
    }

    public class SuccessMessage
    {
        public const string RequestSuccessful = "Request successful.";

        public const string LoginSuccessfully = "Login successfully.";
        public static readonly Func<string, string> SuccessfullyMessage = (entityName) => $"{entityName} successfully.";
    }
}