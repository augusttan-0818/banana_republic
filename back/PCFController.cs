using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.PCF;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [ApiController]
    [Route("api/v1/pcf")]
    public class PCFController : ControllerBase
    {
        private readonly IPCFService _service;

        public PCFController(IPCFService service)
        {
            _service = service;
        }

        // GET: api/v1/pcf
        [HttpGet]
        public async Task<ActionResult<List<GetPCFTrackingRecordDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/v1/pcf/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPCFTrackingRecordDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/v1/pcf
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePCFRequest request)
        {
            var id = await _service.CreateAsync(request);

            return Ok(id);
        }

        // PUT: api/v1/pcf/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePCFRequest request)
        {
            var exists = await _service.ExistsAsync(id);
            if (!exists) return NotFound();

            await _service.UpdateAsync(id, request);
            return NoContent();
        }

        // DELETE: api/v1/pcf/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _service.ExistsAsync(id);
            if (!exists) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
