namespace NRC.Const.CodesAPI.Application.DTOs.AppDTOs.ReferenceDocumentUpdate
{
    public class AgencyDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? NameFR { get; set; }
        public string Abbr { get; set; } = string.Empty;
        public string? AbbrFR { get; set; }
        public bool? SCC { get; set; }
    }
}

