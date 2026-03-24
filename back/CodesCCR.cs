using NRC.Const.CodesAPI.Domain.Common.Base;
using NRC.Const.CodesAPI.Domain.Entities.Core;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;

namespace NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests
{
    public class CodesCCR : AuditableEntity
    {
        public required int CCRId { get; set; }
        public short? CodesCycleId { get; set; }
        public CodesCycle? CodesCycle { get; set; }
        public byte? ChangeTypeId { get; set; }
        public CodesCCRCodeChangeType? ChangeType { get; set; }
        public short? FormatTypeId { get; set; }
        public CodesCCRFormatType? FormatType { get; set; }
        public int? CCRNumber { get; set; }
        public string? CCRTitle { get; set; }
        public string? CCRTitleFR { get; set; }

        public byte? CCRInternalStatusId { get; set; }
        public CodesCCRInternalStatus? CCRInternalStatus { get; set; }
        public byte? CCRSubmissionStatus { get; set; }
        public string? CodeReference { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Comments { get; set; }
        public string? Language { get; set; }
        public byte? NotifyPreference { get; set; }
        public byte? ProponentType { get; set; }
        public string? ChangeSolution { get; set; }
        public string? EnforcementImplications { get; set; }
        public bool? CBHCCApproval { get; set; }
        public string? ImpactCosts { get; set; }
        public string? ImpactBenefits { get; set; }
        public bool? Notify { get; set; }
        public string? NewProvisionSolution { get; set; }
        public string? OtherProponentType { get; set; }
        public string? Problem { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? WithdrawalDate { get; set; }
        public string? Subject { get; set; }
        public string? ChangeSolutionFR { get; set; }
        public string? EnforcementImplicationsFR { get; set; }
        public string? ImpactCostsFR { get; set; }
        public string? ImpactBenefitsFR { get; set; }
        public string? NewProvisionSolutionFR { get; set; }
        public string? ProblemFR { get; set; }
        public string? SubjectFR { get; set; }
        public string? Justification { get; set; }
        public string? JustificationFR { get; set; }
        public string? OtherProponentTypeFR { get; set; }
        public int? Requestor { get; set; }
        public string? CodeReferenceFR { get; set; }
        public string? CommentsFR { get; set; }
        public int? TeamLead { get; set; }
        public int? LeadTA { get; set; }
        public CodesResource? LeadTAResource { get; set; }

        public bool? SubmittedByFPT { get; set; }
        public short? SortingOutputId { get; set; }
        public int? SortingCommitteesConcernedId { get; set; }
        public string? InternalOverallComment { get; set; }
        public bool? DevelopmentCompleted { get; set; }
        public required int CodesCCRProponentId { get; set; }
        public required CodesCCRProponent CodesCCRProponent { get; set; }

        public int? UnifiedContactId { get; set; }
        public UnifiedContact? UnifiedContact { get; set; }

        public string? CCRDescription { get; set; }
        public string? CCRDescriptionFR { get; set; }
        public DateTime? TriageDate { get; set; }
        public short? TriageOutputId { get; set; }
        public int? TriageCommitteesConcernedId { get; set; }
        public string? TriageComment { get; set; }

        public DateTime? AnalysisDate { get; set; }
        public int? AnalysisTypeId { get; set; }
        public string? Analysis { get; set; }
        public string? AnalysisFR { get; set; }
        public string? AnalysisNote { get; set; }
        public int? AnalysisCommitteeId { get; set; }


        public DateTime? PresentDate { get; set; }
        public string? MeetingMinutes { get; set; }

        public string? DiscussionNotes { get; set; }
        public string? PresentCommnets { get; set; }
        public int? PresentDecisionId { get; set; }

        public short? CurrentStatusId { get; set; }

        public ICollection<CodesCCRStatusHistory> StatusHistory { get; set; }
            = new List<CodesCCRStatusHistory>();

    }
}
