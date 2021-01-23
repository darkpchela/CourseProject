namespace BusinessLayer.Interfaces.Authentication
{
    public interface ISessionHelper
    {
        int GetCurrentUserId();

        void RememberUserId(int id);

        int GetRemeberedUserId();
    }
}