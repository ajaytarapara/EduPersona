namespace EduPersona.Core.Shared.Constants
{
    public static class Messages
    {
        public static string EmailRequired => "Email is required.";
        public static string FirstNameRequired => "First name is required.";
        public static string LastNameRequired => "Last name is required.";
        public static string PasswordRequired => "Password is required.";
        public static string ConfirmPasswordRequired => "Confirm password is required.";
        public static string InvalidEmail => "Please enter a valid email address.";
        public static string PasswordMismatch => "Passwords do not match.";
        public static string EmailMaxLength => "Email cannot exceed 100 characters.";
        public static string NameMaxLength => "Name cannot exceed 50 characters.";
        public static string PasswordMinLength => "Password must be at least 6 characters long.";
        public static string PasswordRegex =>
           "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character.";
        public static string AccessTokenGenerateError => "Your session has expired or invalid. Please try again.";
        public const string LoginSuccessfully = "Login successfully.";
        public static readonly Func<string, string> NotFoundMessage = (entityName) => $"{entityName} not found.";
        public const string RefreshTokenExpired = "Refresh token expired.";
        public const string RequestSuccessful = "Request successful.";
        public static readonly Func<string, string> SuccessfullyMessage = (entityName) => $"{entityName} successfully.";
    }
}
