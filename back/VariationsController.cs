using Asp.Versioning;
using AutoMapper;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Variations;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Variations;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Core;
using NRC.Const.CodesAPI.API.Auth;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/variations/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class VariationsController(IVariationsRepository variationsRepository, IMapper mapper) : ControllerBase
    {
        private readonly IVariationsRepository _variationsRepository = variationsRepository?? throw new ArgumentNullException(nameof(variationsRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        const int _maxVariationsPageSize = 20;
        /// <summary>
        /// Get code variations given required params - CodeYear, CodeBook, CodeDivision, PartNumber.
        /// Optional params - SectionNumber, SubSectionNumber, ProvinceList
        /// </summary>
        /// <param name="qryVariations"></param>
        /// <returns></returns>
        [HttpPost("QueryVariations")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeVariationsResponse>>> GetVariations(
                VariationsGetRequest qryVariations)
        {
                var variationEntities = await _variationsRepository.GetCodesVariationsAsync(qryVariations);
                return Ok(_mapper.Map<IEnumerable<GetCodeVariationsResponse>>(variationEntities));
            
        }

        [AllowAnonymous]
        [HttpGet("health")]
        public IActionResult HealthCheck() => Ok("Alive");

        /// <summary>
        /// Get code variations given required params - CodeYear, CodeBook, CodeDivision, PartNumber.
        /// Optional params - SectionNumber, SubSectionNumber, ProvinceList
        /// </summary>
        /// <param name="qryVariations">Reqd - CodeYear, CodeBook, CodeDivision, PartNumber; Optional - SectionNumber, SubSectionNumber, ProvinceList</param>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Total of records to display in a page</param>
        /// <returns></returns>
        [HttpPost("QueryPagedVariations")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeVariationsResponse>>> GetPagedVariations(
                VariationsGetRequest qryVariations, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > _maxVariationsPageSize)
            {
                pageSize = _maxVariationsPageSize;
            }
            var (variationEntities, paginationMetadata) = await _variationsRepository.GetPagedCodesVariationsAsync(qryVariations, pageNumber, pageSize);
            Response.Headers.Append("X-Pagination",  JsonSerializer.Serialize(paginationMetadata));
            return Ok(_mapper.Map<IEnumerable<GetCodeVariationsResponse>>(variationEntities));

        }

        /// <summary>
        /// Get code variations given required params - CodeYear, VariationLabel
        /// </summary>
        /// <param name="qryVariations">CodeYear, VariationLabel</param>
        /// <returns>Code Variations</returns>
        [HttpPost("QueryVariationsByVariationLabel")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeVariationsByVariationLabelResponse>>> GetVariationsByVariationLabel(
               VariationsByVariationLabelGetRequest qryVariations)
        {
            var variationEntities = await _variationsRepository.GetCodesVariationsByVariationLabelAsync(qryVariations);
            return Ok(_mapper.Map<IEnumerable<GetCodeVariationsByVariationLabelResponse>>(variationEntities));

        }


        /// <summary>
        /// Get Provinces for code variations
        /// </summary>
        /// <param name="codeYear">Code year for the variations</param>
        /// <returns></returns>
        [HttpGet("CodeProvinces")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeProvincesResponse>>> GetCodeProvinces(int codeYear)
        {
            var codeProvincesEntities = await _variationsRepository.GetCodesVariationsProvincesAsync(codeYear);
            return Ok(_mapper.Map<IEnumerable<GetCodeProvincesResponse>>(codeProvincesEntities));

        }
        /// <summary>
        /// Get code years that we have variations for
        /// </summary>
        /// <returns></returns>
        [HttpGet("CodeYears")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeYearsResponse>>> GetCodeYears()
        {
            var codeYearsEntities = await _variationsRepository.GetCodeYearsAsync();
            return Ok(_mapper.Map<IEnumerable<GetCodeYearsResponse>>(codeYearsEntities));

        }

        [HttpGet("CodeMappingByJurisdiction")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeMappingByJurisdictionResponse>>> GetCodeMappingByJurisdiction()
        {
            var codemappingEntities = await _variationsRepository.GetCodeMappingByJurisdictionAsync();
            return Ok(_mapper.Map<IEnumerable<GetCodeMappingByJurisdictionResponse>>(codemappingEntities));

        }

        /// <summary>
        /// Get codebooks for the given codeyear
        /// </summary>
        /// <param name="codeYear">Code year for the variations</param>
        /// <returns></returns>
        [HttpGet("CodeBooks")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeBooksResponse>>> GetCodeBooks(int codeYear)
        {
            var codeBooksEntities = await _variationsRepository.GetCodeBooksAsync(codeYear);
            return Ok(_mapper.Map<IEnumerable<GetCodeBooksResponse>>(codeBooksEntities));

        }

        /// <summary>
        /// Get Variation Labels for the given codeyear
        /// </summary>
        /// <param name="codeYear">Code year for which you want to get variation labels</param>
        /// <returns></returns>
        [HttpGet("VariationLabels")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetVariationLabelsResponse>>> GetVariationLabels(int codeYear)
        {
            var variationLabelsEntities = await _variationsRepository.GetVariationLabelsAsync(codeYear);
            return Ok(_mapper.Map<IEnumerable<GetVariationLabelsResponse>>(variationLabelsEntities));

        }

        /// <summary>
        /// Get code divisions for the supplied codeyear and codebook
        /// </summary>
        /// <param name="codeYear">Code year for the variations</param>
        /// <param name="codeBook">Code book for the variations</param>
        /// <returns></returns>
        [HttpGet("CodeDivisions")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetCodeDivisionsResponse>>> GetCodeDivisions(int codeYear, string codeBook)
        {
            var codeDivisionsEntities = await _variationsRepository.GetCodesDivisionsAsync(codeYear, codeBook);
            return Ok(_mapper.Map<IEnumerable<GetCodeDivisionsResponse>>(codeDivisionsEntities));

        }

        /// <summary>
        /// Get partnumbers given codeyear, codebook and codedivision
        /// </summary>
        /// <param name="codeYear">Code year for the variations</param>
        /// <param name="codeBook">Code book for the variations</param>
        /// <param name="codeDivision">Division for the variations</param>
        /// <returns></returns>
        [HttpGet("GetPartNumbers")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetPartNumbersResponse>>> GetPartNumbers(int codeYear, string codeBook, string codeDivision)
        {
            var partNumbersEntities = await _variationsRepository.GetPartNumbersAsync(codeYear, codeBook, codeDivision);
            return Ok(_mapper.Map<IEnumerable<GetPartNumbersResponse>>(partNumbersEntities));

        }

        /// <summary>
        /// Get section number for the given codeyear, codebook, codedivision and partnumber
        /// </summary>
        /// <param name="codeYear">Code year for the variations</param>
        /// <param name="codeBook">Code book for the variations</param>
        /// <param name="codeDivision">Division for the variations</param>
        /// <param name="partNumber">Partnumber for the variations</param>
        /// <returns></returns>
        [HttpGet("SectionNumbers")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetSectionNumbersResponse>>> GetSectionNumbers(int codeYear, string codeBook, string codeDivision, string partNumber)
        {
            var sectionNumbersEntities = await _variationsRepository.GetSectionNumbersAsync(codeYear, codeBook, codeDivision, partNumber);
            return Ok(_mapper.Map<IEnumerable<GetSectionNumbersResponse>>(sectionNumbersEntities));

        }

        /// <summary>
        /// Get subsectionnumbers for the given codeyear, codebook, codedivision, partnumber, sectionnumber
        /// </summary>
        /// <param name="codeYear">Code year for the variations</param>
        /// <param name="codeBook">Code book for the variations</param>
        /// <param name="codeDivision">Division for the variations</param>
        /// <param name="partNumber">Partnumber for the variations</param>
        /// <param name="sectionNumber">Sectionnumber for the variations</param>
        /// <returns></returns>
        [HttpGet("SubSectionNumbers")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetSubSectionNumbersResponse>>> GetSubSectionNumbers(int codeYear, string codeBook, string codeDivision, string partNumber, string sectionNumber)
        {
            var subSectionNumbersEntities = await _variationsRepository.GetSubSectionNumbersAsync(codeYear, codeBook, codeDivision, partNumber, sectionNumber);
            return Ok(_mapper.Map<IEnumerable<GetSubSectionNumbersResponse>>(subSectionNumbersEntities));

        }
    }
}

