namespace BusinessLayer.Interfaces
{
    public interface IAuthenticatableModel
    {
        bool IsAdminRequest { get; set; }

        int RequesterId { get; set; }

        int OwnerId { get; set; }
    }
}