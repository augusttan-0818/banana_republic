namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.Search
{
    public class RdUpdateSearchQuery
    {
        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Update ID Range
        public int? DocumentNumberFrom { get; set; }
        public int? DocumentNumberTo { get; set; }

        // Date Submitted Range
        public DateTime? SubmittedDateFrom { get; set; }
        public DateTime? SubmittedDateTo { get; set; }

        // Status Section
        public string? Having { get; set; }  // "Having" or "Not having"
        public string? StatusValue { get; set; }
        public string? Decision { get; set; }
        public string? StatusCommittee { get; set; }
        public string? MinutesReference { get; set; }

        // Additional Section
        public string? AdditionalCommittee { get; set; }
        public string? AdditionalAgency { get; set; }
        public string? AdditionalCode { get; set; }
        public string? AdditionalCodeReference { get; set; }
        public string? PublicReview { get; set; }  // "Post", "Conflict", or empty
        public string? IncludeExclude { get; set; }  // "Include" or "Exclude"
    }
}
