using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.PCF;
using NRC.Const.CodesAPI.Domain.Entities.PCF;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesPCFProfile : Profile
    {
        public CodesPCFProfile()
        {
            CreateMap<CreatePCFRequest, PCF>()
                .ForMember(dest => dest.CodeCycleId, opt => opt.MapFrom(src => (short)1))  // default value
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
                .ReverseMap();

            CreateMap<UpdatePCFRequest, PCF>()
                .ForMember(dest => dest.PcfNumber, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PCFCodeReference, PCFCodeReferenceDto>().ReverseMap();

            CreateMap<PCF, GetPCFTrackingRecordDto>()
                .ForMember(dest => dest.PCFId, opt => opt.MapFrom(src => src.PCFId))
                .ForMember(dest => dest.PCFNumber, opt => opt.MapFrom(src => src.PcfNumber.ToString()))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src =>
                    src.Subject.HasValue ? src.Subject.Value.ToString() : null))
                .ForMember(dest => dest.CodeReference, opt => opt.MapFrom(src =>
                    src.PCFCodeReferences.FirstOrDefault() != null
                        ? src.PCFCodeReferences.First().CodeReference
                        : null))
                .ForMember(dest => dest.RelatedCommittee, opt => opt.MapFrom(src =>
                    src.RelatedCommittee != null ? src.RelatedCommittee.CommitteeName : null))
                .ForMember(dest => dest.TargetPR, opt => opt.MapFrom(src =>
                    src.TargetPR != null ? src.TargetPR.PublicReviewTitle : null))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src =>
                    src.AssignedTo.HasValue ? src.AssignedTo.Value.ToString() : null))
                .ForMember(dest => dest.Committee, opt => opt.MapFrom(src =>
                    src.RelatedCommittee != null ? src.RelatedCommittee.CommitteeName : null))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.Status != null ? src.Status.StatusName : null))
                .ForMember(dest => dest.Subject_Eng, opt => opt.MapFrom(src =>
                    src.SubjectNavigation != null ? src.SubjectNavigation.Subject_Eng : null))
                .ForMember(dest => dest.Subject_Fr, opt => opt.MapFrom(src =>
                    src.SubjectNavigation != null ? src.SubjectNavigation.Subject_Fr : null));


            CreateMap<PCF, GetPCFResponse>()
                .ForMember(dest => dest.PcfId, opt => opt.MapFrom(src => src.PCFId))
                .ForMember(dest => dest.PcfNumber, opt => opt.MapFrom(src => src.PcfNumber.ToString()))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src =>
                    src.Subject.HasValue ? src.Subject.Value.ToString() : null))

                .ForMember(dest => dest.CodeReferenceId, opt => opt.MapFrom(src =>
                    src.CodeReferenceId != null ? int.Parse(src.CodeReferenceId) : (int?)null))

                .ForMember(dest => dest.CodeReferenceName, opt => opt.MapFrom(src =>
                    src.PCFCodeReferences.FirstOrDefault() != null
                        ? src.PCFCodeReferences.First().CodeReference
                        : null))

                .ForMember(dest => dest.RelatedCommitteeId, opt => opt.MapFrom(src => src.RelatedCommitteeId))
                .ForMember(dest => dest.RelatedCommitteeName, opt => opt.MapFrom(src =>
                    src.RelatedCommittee != null ? src.RelatedCommittee.CommitteeName : null))

                .ForMember(dest => dest.TargetPRId, opt => opt.MapFrom(src => src.TargetPRId))
                .ForMember(dest => dest.TargetPRName, opt => opt.MapFrom(src =>
                    src.TargetPR != null ? src.TargetPR.PublicReviewTitle : null))

                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src =>
                    src.AssignedTo.HasValue ? src.AssignedTo.Value.ToString() : null))

                .ForMember(dest => dest.Committee, opt => opt.MapFrom(src =>
                    src.RelatedCommittee != null ? src.RelatedCommittee.CommitteeName : null))

                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.Status != null ? src.Status.StatusName : null))

                .ForMember(dest => dest.LeadTA, opt => opt.MapFrom(src =>
                    src.LeadTA.HasValue ? src.LeadTA.Value.ToString() : null))

                .ForMember(dest => dest.ActualPR1, opt => opt.MapFrom(src =>
                    src.ActualPR1.HasValue ? src.ActualPR1.Value.ToString() : null))

                .ForMember(dest => dest.ActualPR2, opt => opt.MapFrom(src =>
                    src.ActualPR2.HasValue ? src.ActualPR2.Value.ToString() : null));
        }
    }
}
