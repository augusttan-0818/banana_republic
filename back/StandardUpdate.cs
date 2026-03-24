using System.ComponentModel.DataAnnotations.Schema;
using NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate;

namespace NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate
{
    [Table("StandardUpdate")]
    public class StandardUpdate
    {
        public int Id { get; set; } 
        public required string StandardId { get; set; } 
        public Standard? Standard { get; set; } 
        public required string DocNumber { get; set; } 
        public required string Title { get; set; } 
        public string? Change { get; set; }
        public string? Rationale { get; set; }
        public string? Impact { get; set; } 
        public DateTime? PublicationDate { get; set; }
        public required DateTime RequestDate { get; set; }
        public required string UpdatedBy { get; set; } 
        public required string Lang { get; set; } 
        public string? StandardId2 { get; set; } 
        public string? DocNumber2 { get; set; }
        public string? Title2 { get; set; } 
        public DateTime? WithdrawnDate { get; set; }  
        public required bool IsWithdrawn { get; set; } 
        public string? ImpactFre { get; set; } 
        public string? ChangeFre { get; set; }  
        public string? RationaleFre { get; set; } 
        public DateTime? LastUpdatedDate { get; set; } 
        public string? LastUpdatedBy { get; set; }
        public string? InternalNotes { get; set; }      
        public string? WfNotes { get; set; }
        public string? SuppDoc1 { get; set; }
        public string? SuppDoc2 { get; set; } 
        public DateTime? PublicationDate2 { get; set; }
        public DateTime? WithdrawnDate2 { get; set; }
        public string? PrDocNumber { get; set; } 
        public string? PrDocNumber2 { get; set; } 
        public string? PrTitle { get; set; }  
        public string? PrTitle2 { get; set; } 
        public DateTime? PrWithdrawnDate { get; set; }  
        public DateTime? PrWithdrawnDate2 { get; set; }  
        public string? OrigDocNumber { get; set; }  
        public string? OrigDocNumber2 { get; set; } 
        public string? OrigTitle { get; set; } 
        public string? OrigTitle2 { get; set; }  
        public string? PcfNoteEng { get; set; }  
        public string? PcfNoteFre { get; set; }  
        public string? SdoDocNumber { get; set; } 
        public string? SdoDocNumber2 { get; set; }  
        public string? SdoTitle { get; set; } 
        public string? SdoTitle2 { get; set; }  
        public string? PrPcfNoteEng { get; set; }  
        public string? PrPcfNoteFre { get; set; } 
        public string? NewAgency { get; set; }
    }
}
