using AutoMapper;
using NRC.Const.CodesAPI.API.Helpers;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Variations;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Variations;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodeVariationResolver : IValueResolver<GetCodeVariations_Result, GetCodeVariationsResponse, string?>
    {
        public string? Resolve(GetCodeVariations_Result source, GetCodeVariationsResponse destination, string? member, ResolutionContext context)
        {
            return DiffHelper.wordDiff(source.NationalsentenceText ,source.ProvinceSentenceText);
        }
    }
}

