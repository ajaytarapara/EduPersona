namespace IdentityProvider.Shared.Models.Helper
{
    public class GenerateTokenRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}