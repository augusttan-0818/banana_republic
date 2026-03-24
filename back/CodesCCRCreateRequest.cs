using NRC.Const.CodesAPI.Domain.Entities.Core;

namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests
{
    public class CodesCCRCreateRequest
    {
        public short? CodesCycleId { get; set; }
        public CodesCycle? CodesCycle { get; set; }
        public byte? ChangeTypeId { get; set; }
        public short? FormatTypeId { get; set; }
        public string? CodeReference { get; set; }
        public string? Comments { get; set; }
        public string? Language { get; set; }
        public byte? ProponentType { get; set; }
        public string? ChangeSolution { get; set; }
        public string? EnforcementImplications { get; set; }
        public string? ImpactCosts { get; set; }
        public string? ImpactBenefits { get; set; }
        public string? NewProvisionSolution { get; set; }
        public string? OtherProponentType { get; set; }
        public string? Problem { get; set; }
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
        public bool? SubmittedByFPT { get; set; }
        public required int ProponentId { get; set; }
    }
}
