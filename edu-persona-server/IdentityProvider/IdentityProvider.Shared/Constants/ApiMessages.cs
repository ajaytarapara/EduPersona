namespace IdentityProvider.Shared.Constants
{
    public class ApiMessages
    {
        public const string UserRegisteredSuccessfully = "User registered successfully.";
        public const string UserAlreadyExists = "User already exists.";
        public static readonly Func<string, string> NotFoundMessage = (entityName) => $"{entityName} not found.";
        public const string InvalidPassword = "Invalid password.";
        public const string LoginSuccessfully = "Login successfully.";
    }
}
