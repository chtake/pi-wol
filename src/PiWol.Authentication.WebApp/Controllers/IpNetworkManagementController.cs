using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PiWol.Authentication.Abstraction.Services;
using PiWol.Authentication.WebApp.Dtos;
using PiWol.Authentication.WebApp.Mapping;

namespace PiWol.Authentication.WebApp.Controllers
{
    [Route("api/access/[controller]")]
    public class IpNetworkManagementController : Controller
    {
        private readonly IIpNetworkService _service;

        public IpNetworkManagementController(IIpNetworkService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _service.GetAll().ConfigureAwait(true);

            return Ok(models);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostIpNetworkDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _service.Create(dto.ToModel()).ConfigureAwait(false);

            return CreatedAtAction("GetAll", created);
        }

        [HttpPost("check")]
        public async Task<IActionResult> PostCheck([FromBody] PostIpNetworkDto dto)
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult(NotFound()).ConfigureAwait(true);
            }

            var resultDto = new PostIpNetworkDto
            {
                IpNetwork = _service.Parse(dto.ToModel())
            };

            return await Task.FromResult(Ok(resultDto)).ConfigureAwait(true);
        }

        [HttpDelete("{ipNetwork}")]
        public async Task<IActionResult> Delete(string ipNetwork)
        {
            var deleted = await _service.Delete(WebUtility.UrlDecode(ipNetwork)).ConfigureAwait(false);

            return Ok(deleted);
        }
    }
}