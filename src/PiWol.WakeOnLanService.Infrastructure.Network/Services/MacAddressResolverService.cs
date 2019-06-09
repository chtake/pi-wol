using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PiWol.WakeOnLanService.Abstraction.Services;

namespace PiWol.WakeOnLanService.Infrastructure.Network.Services
{
    internal class MacAddressResolverService : IMacAddressResolverService
    {
        private static readonly Regex Regex = new Regex(@"([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})");

        private readonly ILogger _logger;

        private readonly IHostConnectivityCheckerService _pingService;

        public MacAddressResolverService(IHostConnectivityCheckerService pingService, ILoggerFactory loggerFactory)
        {
            _pingService = pingService;
            _logger = loggerFactory.CreateLogger(GetType().Name);
        }

        public async Task<string> Resolve(string ipAddr)
        {
            await _pingService.CheckHost(ipAddr).ConfigureAwait(false);

            var psi = new ProcessStartInfo
            {
                FileName = "arp",
                Arguments = $"-a {ipAddr}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var proc = new Process { StartInfo = psi })
            {
                proc.Start();

                proc.WaitForExit();

                var text = proc.StandardOutput.ReadToEnd();

                var match = Regex.Match(text);

                if (match.Success)
                {
                    return match.Groups.FirstOrDefault()?.Value;
                }
            }

            return string.Empty;
        }
    }
}