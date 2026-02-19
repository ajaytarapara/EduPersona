namespace IdentityProvider.Shared.Models.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public int SessionId { get; set; }
        public UserInfo UserInfo { get; set; } = null!;

    }
}

public class UserInfo
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string Role { get; set; } = null!;
}

