using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Core;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Variations;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Core;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Variations;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodeVariationsProfile: Profile
    {
        public CodeVariationsProfile()
        {
            CreateMap<GetCodeVariations_Result, GetCodeVariationsResponse>()
                .ForMember(dest => dest.TextDifference, opt => opt.MapFrom<CodeVariationResolver>());
            CreateMap<GetCodeBooks_Result, GetCodeBooksResponse>();
            CreateMap<GetCodeYears_Result, GetCodeYearsResponse>();
            CreateMap<GetCodeDivisions_Result, GetCodeDivisionsResponse>();
            CreateMap<GetCodeProvinces_Result, GetCodeProvincesResponse>();
            CreateMap<GetPartNumbers_Result, GetPartNumbersResponse>();
            CreateMap<GetSectionNumbers_Result, GetSectionNumbersResponse>();
            CreateMap<GetSubSectionNumbers_Result, GetSubSectionNumbersResponse>();
            CreateMap<GetVariationsByVariationLabel_Result, GetCodeVariationsByVariationLabelResponse>()
                .ForMember(dest => dest.TextDifference, opt => opt.MapFrom<CodeVariationByVariationLabelResolver>());
            CreateMap<GetCodeMappingByJurisdiction_Result, GetCodeMappingByJurisdictionResponse>();
            CreateMap<GetVariationLabels_Result, GetVariationLabelsResponse>();
        }
        
     
    }
}

