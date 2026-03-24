namespace NRC.Const.CodesAPI.Application.DTOs.AppDTOs.ReferenceDocumentUpdate
{
    public class StandardDto
    {
        public string Id { get; set; } = string.Empty;
        public string AgencyId { get; set; } = string.Empty;
        public AgencyDto? Agency { get; set; }
        public string? DocNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Lang { get; set; } = string.Empty;
        public string? OtherLangId { get; set; }
        public string? OtherAgency { get; set; }
    }
}

