using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate
{
    [Table("StandardUpdateSubStatus")]
    public class StandardUpdateSubStatus
    {
        [Key]
        public int Id { get; set; }

        [Column("StandardUpdateSubStatus")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("StandardUpdateSubStatusFR")]
        [MaxLength(100)]
        public string? NameFR { get; set; }

        public int? StandardUpdateStatusId { get; set; }

        public int? SortOrder { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public bool IsRetired { get; set; }

        public DateTime? RetiredOn { get; set; }

        // Navigation property
        [ForeignKey("StandardUpdateStatusId")]
        public StandardUpdateStatus? Status { get; set; }
    }
}
