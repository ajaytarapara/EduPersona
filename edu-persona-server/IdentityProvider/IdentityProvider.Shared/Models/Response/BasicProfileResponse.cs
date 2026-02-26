namespace IdentityProvider.Shared.Models.Response
{
    public class BasicProfileResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;
        public string? GoogleId { get; set; }
    }
}
