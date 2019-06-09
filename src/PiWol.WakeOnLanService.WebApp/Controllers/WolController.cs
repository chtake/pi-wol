using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PiWol.WakeOnLanService.Abstraction.Services;

namespace PiWol.WakeOnLanService.WebApp.Controllers
{
    [Route("api/wol/[controller]")]
    public class WolController : Controller
    {
        private readonly IHostService _hostService;

        private readonly IWakeUpService _wolService;

        public WolController(IWakeUpService wolService, IHostService hostService)
        {
            _wolService = wolService;
            _hostService = hostService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> WakeUpHost(Guid id)
        {
            var host = await _hostService.Read(id).ConfigureAwait(false);
            if (host == null)
            {
                return NotFound();
            }

            await _wolService.SendMagicPacket(host).ConfigureAwait(false);
            return Ok();
        }
    }
}