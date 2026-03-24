using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codechangetype/")]
    [ApiController]
    //  [Authorize]
    [ApiVersion(1)]
    public class CodeChangeTypesController(ICodeChangeTypeRepository codeChangeTypeRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICodeChangeTypeRepository _codeChangeTypeRepository = codeChangeTypeRepository ?? throw new ArgumentNullException(nameof(codeChangeTypeRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CodesCCRCodeChangeType>>> GetCodeChangeTypes()
        {
            var output = await _codeChangeTypeRepository.GetCodeChangeTypes();
            return Ok(output);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<CodesCCRCodeChangeType>> GetCodeChangeType(byte id)
        {
            var codeChangeType = await _codeChangeTypeRepository.GetCodeChangeType(id);

            if (codeChangeType == null)
            {
                return NotFound();
            }

            return Ok(codeChangeType);
        }

    }
}
