using AutoMapper;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesCycle;

public class CodesCycleProfile : Profile
{
    public CodesCycleProfile()
    {
        CreateMap<CodesCycle, CodesCycleDto>();
    }
}
