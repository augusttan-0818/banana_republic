using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codesccrformattypes/")]
    [ApiController]
    //  [Authorize]
    [ApiVersion(1)]
    public class CodesCCRFormatTypesController(ICodesCCRFormatTypeRepository codesCCRFormatTypeRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICodesCCRFormatTypeRepository _codesCCRFormatTypeRepository = codesCCRFormatTypeRepository ?? throw new ArgumentNullException(nameof(codesCCRFormatTypeRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CodesCCRFormatType>>> GetCodeChangeTypes()
        {
            var output = await _codesCCRFormatTypeRepository.GetCodesCCRFormatTypes();
            return Ok(output);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<CodesCCRFormatType>> GetCodeChangeType(short id)
        {
            var formatType = await _codesCCRFormatTypeRepository.GetCodesCCRFormatType(id);

            if (formatType == null)
            {
                return NotFound();
            }

            return Ok(formatType);
        }
    }
}
