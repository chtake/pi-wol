using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IAuthenticationService = PiWol.Authentication.Abstraction.Services.IAuthenticationService;

namespace PiWol.Authentication.Handler
{
    public class PiWolAuthenticationHandler : AuthenticationHandler<PiWolAuthenticationOptions>
    {
        private readonly IAuthenticationService _authenticationService;

        public PiWolAuthenticationHandler(
            IAuthenticationService authenticationService,
            IOptionsMonitor<PiWolAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (_authenticationService.IsAuthenticatedUser())
            {
                var identity =
                    new ClaimsIdentity(Enumerable.Empty<Claim>(), AuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal,
                    AuthenticationDefaults.AuthenticationScheme))).ConfigureAwait(false);
            }

            return await Task.FromResult(AuthenticateResult.Fail("Your authentication could not be verified."))
                .ConfigureAwait(false);
        }
    }
}