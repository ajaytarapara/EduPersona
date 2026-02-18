using System.Security.Cryptography;
using System.Text;

namespace IdentityProvider.Shared.Helper
{
    public static class PasswordHelper
    {
        public static string CreateHash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool Verify(string enteredPassword, string storedHash)
        {
            var hashOfInput = CreateHash(enteredPassword);
            return hashOfInput == storedHash;
        }
    }
}
