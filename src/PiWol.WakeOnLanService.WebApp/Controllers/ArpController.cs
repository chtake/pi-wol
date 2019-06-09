using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.WebApp.Dto;

namespace PiWol.WakeOnLanService.WebApp.Controllers
{
    [Route("api/wol/[controller]")]
    public class ArpController : Controller
    {
        private readonly IMacAddressResolverService _service;

        public ArpController(IMacAddressResolverService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [RegularExpression(@"\b(?:(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.){3}(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\b")]
            [Required]
            [FromQuery]
            string ip)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var mac = await _service.Resolve(ip).ConfigureAwait(false);

            return Ok(new ArpDto { IpAddress = ip, MacAddress = (string.IsNullOrEmpty(mac) ? string.Empty : mac) });
        }
    }
}