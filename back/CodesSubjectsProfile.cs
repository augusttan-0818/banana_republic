using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.NewFolder;
using NRC.Const.CodesAPI.Domain.Entities.Subjects;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectDto>();
    }
}
