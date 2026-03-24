namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests
{
    public class GetCodesCCRs_Result
    {
        public required int CCRId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CodeReference { get; set; }
        public short? CodesCycleId { get; set; }
        public string CodesCycleName { get; set; }
        public int? CCRNumber { get; set; }
        public byte? CCRInternalStatusId { get; set; }
        public string CCRInternalStatusName { get; set; }
        public byte? CCRSubmissionStatus { get; set; }
        public string CCRSubmissionStatusName { get; set; }
        public string? Subject { get; set; }
        public string? CCRTitle { get; set; }
        public int? LeadTA { get; set; }
        public bool? SubmittedByFPT { get; set; }
        public string ProponentName { get; set; }
        public DateTime? SubmissionDate { get; set; }

        public string LeadTAName { get; set; }
    }
}
