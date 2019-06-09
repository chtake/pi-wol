using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PiWol.WakeOnLanService.Abstraction.Services;
using PiWol.WakeOnLanService.WebApp.Dto;
using PiWol.WakeOnLanService.WebApp.Mapping;

namespace PiWol.WakeOnLanService.WebApp.Controllers
{
    [Route("api/wol/[controller]")]
    public class HostController : Controller
    {
        private readonly IHostService _service;

        public HostController(IHostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ReadAll().ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _service.Read(id).ConfigureAwait(false);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostHostDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.Create(dto.ToModel()).ConfigureAwait(false);

            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PostHostDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var model = dto.ToModel();
            model.Id = id;
            var result = await _service.Update(model).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _service.Read(id).ConfigureAwait(false);
            await _service.Delete(id).ConfigureAwait(false);

            return Ok(model);
        }
    }
}