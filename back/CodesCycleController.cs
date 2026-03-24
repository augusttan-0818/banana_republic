using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v1/codescycle")]
    [ApiController]
    [Authorize]
    public class CodesCycleController : ControllerBase
    {
        private readonly ICodesCycleRepository _codesCycleRepository;
        private readonly IMapper _mapper;

        public CodesCycleController(ICodesCycleRepository codesCycleRepository, IMapper mapper)
        {
            _codesCycleRepository = codesCycleRepository ?? throw new ArgumentNullException(nameof(codesCycleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CodesCycle>>> GetCodesCycles()
        {
            var cycles = await _codesCycleRepository.GetCodesCycles();
            return Ok(cycles);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<CodesCycle>> GetCodesCycle(short id)
        {
            var cycle = await _codesCycleRepository.GetCodesCycle(id);
            if (cycle == null)
                return NotFound();

            return Ok(cycle);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CodesCycle>> CreateCodesCycle(CodesCycle codesCycle)
        {
            var createdCycle = await _codesCycleRepository.CreateCodesCycle(codesCycle);
            return CreatedAtAction(nameof(GetCodesCycle), new { id = createdCycle.CodesCycleId }, createdCycle);
        }
    }
}
