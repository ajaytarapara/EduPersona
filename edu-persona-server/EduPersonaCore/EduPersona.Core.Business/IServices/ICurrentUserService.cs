namespace EduPersona.Core.Business.IServices
{
    public interface ICurrentUserService
    {
        int GetCurrentUserId();
        string GetCurrentUserRole();
    }
}