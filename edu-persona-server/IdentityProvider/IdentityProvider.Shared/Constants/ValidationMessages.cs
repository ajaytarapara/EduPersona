namespace IdentityProvider.Shared.Constants
{
    public static class ValidationMessages
    {
        public const string EmailRequired = "Email is required.";
        public const string FirstNameRequired = "First name is required.";
        public const string LastNameRequired = "Last name is required.";
        public const string PasswordRequired = "Password is required.";
        public const string ConfirmPasswordRequired = "Confirm password is required.";
        public const string InvalidEmail = "Please enter a valid email address.";
        public const string PasswordMismatch = "Passwords do not match.";
        public const string EmailMaxLength = "Email cannot exceed 100 characters.";
        public const string NameMaxLength = "Name cannot exceed 50 characters.";
        public const string PasswordMinLength = "Password must be at least 6 characters long.";
        public const string PasswordRegex =
            "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character.";

    }
}
