using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Infrastructure.Services.Repositories;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codesccrsortingoutputs/")]
    [ApiController]
    //  [Authorize]
    [ApiVersion(1)]
    public class CodesCCRSortingOutputsController(ICodeSortingOutputRepository codeSortingOutputRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICodeSortingOutputRepository _codeSortingOutputRepository = codeSortingOutputRepository ?? throw new ArgumentNullException(nameof(codeSortingOutputRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CodesCCRSortingOutput>>> GetCodeSortingOutputs()
        {
            var output = await _codeSortingOutputRepository.GetCodeCCRSortingOutputs();
            return Ok(output);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<CodesCCRSortingOutput>> GetCodeSortingOutput(short id)
        {
            var sortingOutput = await _codeSortingOutputRepository.GetCodeCCRSortingOutput(id);

            if (sortingOutput == null)
            {
                return NotFound();
            }

            return Ok(sortingOutput);
        }
    }
}
