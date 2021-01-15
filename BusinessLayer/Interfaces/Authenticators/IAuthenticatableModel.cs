namespace BusinessLayer.Interfaces.Authenticators
{
    public interface IAuthenticatableModel
    {
        int OwnerId { get; set; }
    }
}