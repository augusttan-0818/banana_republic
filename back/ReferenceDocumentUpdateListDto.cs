namespace NRC.Const.CodesAPI.Application.DTOs.AppDTOs.ReferenceDocumentUpdate
{
    public class ReferenceDocumentUpdateListDto
    {
        public int Id { get; set; }
        public string DocNumber { get; set; } = string.Empty;
        public string AgencyName { get; set; } = string.Empty;
        public string ReferencedIn { get; set; } = string.Empty; // Empty for now
        public DateTime? WithdrawnDate { get; set; }
        public DateTime? PublicationDate { get; set; }
        public DateTime RequestDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        
        // Empty for now
        public string? SCs { get; set; }
        public string? Status { get; set; }
        public string? PR { get; set; }
        public string? Jobs { get; set; }
    }
}
