using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Variations;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Variations;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Core;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IVariationsRepository
    {
        Task<IEnumerable<GetCodeVariations_Result>> GetCodesVariationsAsync(VariationsGetRequest varRequest);
        Task<(IEnumerable<GetCodeVariations_Result>, PaginationMetadata)> GetPagedCodesVariationsAsync(
       VariationsGetRequest varRequest, int pageNumber, int pageSize);
        Task<IEnumerable<GetCodeProvinces_Result>> GetCodesVariationsProvincesAsync(int codeYear);
        Task<IEnumerable<GetVariationsByVariationLabel_Result>> GetCodesVariationsByVariationLabelAsync(VariationsByVariationLabelGetRequest varRequest);
        Task<IEnumerable<GetCodeYears_Result>> GetCodeYearsAsync();

        Task<IEnumerable<GetCodeMappingByJurisdiction_Result>> GetCodeMappingByJurisdictionAsync();
        Task<IEnumerable<GetCodeBooks_Result>> GetCodeBooksAsync(int codeYear);
        Task<IEnumerable<GetVariationLabels_Result>> GetVariationLabelsAsync(int codeYear);
        Task<IEnumerable<GetCodeDivisions_Result>> GetCodesDivisionsAsync(int codeYear, string codeBook);
        Task<IEnumerable<GetPartNumbers_Result>> GetPartNumbersAsync(int codeYear, string codeBook, string codeDivision);
        Task<IEnumerable<GetSectionNumbers_Result>> GetSectionNumbersAsync(int codeYear, string codeBook, string codeDivision, string partNumber);
        Task<IEnumerable<GetSubSectionNumbers_Result>> GetSubSectionNumbersAsync(int codeYear, string codeBook, string codeDivision, string partNumber, string sectionNumber);

    }
}

