namespace IdentityProvider.Business.IServices
{
    public interface ICurrentUserService
    {
        int GetCurrentUserId();
        string GetCurrentUserRole();
    }
}
