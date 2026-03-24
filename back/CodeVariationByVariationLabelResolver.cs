using AutoMapper;
using NRC.Const.CodesAPI.API.Helpers;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Variations;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Variations;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodeVariationByVariationLabelResolver : IValueResolver<GetVariationsByVariationLabel_Result, GetCodeVariationsByVariationLabelResponse, string?>
    {
        public string? Resolve(GetVariationsByVariationLabel_Result source, GetCodeVariationsByVariationLabelResponse destination, string? member, ResolutionContext context)
        {
            return DiffHelper.wordDiff(source.NationalsentenceText ,source.ProvinceSentenceText);
        }
    }
}

