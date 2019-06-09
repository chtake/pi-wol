using PiWol.Authentication.Abstraction.Services;

namespace PiWol.Authentication.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationMethodService _auth;

        public AuthenticationService(IAuthenticationMethodService auth)
        {
            _auth = auth;
        }

        public bool IsAuthenticatedUser()
        {
            var result = _auth.TryAccessGranted();
            if (result.HasValue == false)
            {
                return true;
            }

            return result.Value;
        }
    }
}