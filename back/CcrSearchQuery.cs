
namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search
{
    public class CcrSearchQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int? FromCcrId { get; set; }
        public int? ToCcrId { get; set; }

        public DateTime? FromDateSubmitted { get; set; }
        public DateTime? ToDateSubmitted { get; set; }

        public int? TeamLeadId { get; set; }
        public int? LeadTAId { get; set; }

        public int? SortingOutputId { get; set; }
        public int? TriageOutputId { get; set; }

        public bool? SubmittedByFPT { get; set; }
        public bool? TriageDateSet { get; set; }

        public DateTime? TriageDate { get; set; }

        public int? SortingCommitteeId { get; set; }
        public int? TriageCommitteeId { get; set; }
    }

}
