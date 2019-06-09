namespace PiWol.Authentication.Abstraction.Services
{
    public interface IAuthenticationMethodService
    {
        bool? TryAccessGranted();
    }
}