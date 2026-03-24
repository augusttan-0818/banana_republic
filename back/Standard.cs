using System.ComponentModel.DataAnnotations.Schema;
using NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate;

namespace NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate
{
    [Table("Standard")]
    public class Standard
    {
        public required string Id { get; set; } 
        public required string AgencyId { get; set; }
        public Agency? Agency { get; set; } 
        public string? DocNumber { get; set; }
        public required string Title { get; set; }
        public required string Lang { get; set; }
        public string? OtherLangId { get; set; }
        public string? OtherAgency { get; set; }
    }
}
