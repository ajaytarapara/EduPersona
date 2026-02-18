namespace IdentityProvider.Shared.Constants
{
    public class RegularExpression
    {
        public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$";
    }
}
