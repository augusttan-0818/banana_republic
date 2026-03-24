using System.Text.Json;
using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesAuditLogs;
using NRC.Const.CodesAPI.Domain.Common.Audit;
using NRC.Const.CodesAPI.Domain.Entities.CodesAuditLogs;
namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesAuditLogProfile : Profile
    {
          private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public CodesAuditLogProfile()
    {
        CreateMap<CodesAuditLog, AuditLogDto>()
            .ForMember(
                dest => dest.Changes,
                opt => opt.MapFrom(src =>
                    JsonSerializer.Deserialize<Dictionary<string, AuditChange>>(
                        src.Changes, _jsonOptions)
                    ?? new Dictionary<string, AuditChange>()));

        CreateMap<AuditLogPagedSource, AuditLogPagedResult>()
            .ForMember(dest => dest.Data,
                opt => opt.MapFrom(src => src.Items))           // triggers AuditLog → AuditLogDto per item
            .ForMember(dest => dest.TotalPages,
                opt => opt.MapFrom(src =>
                    (int)Math.Ceiling(src.Total / (double)src.PageSize)));
    }
    }
}