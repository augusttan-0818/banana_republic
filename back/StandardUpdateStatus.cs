using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NRC.Const.CodesAPI.Domain.Entities.ReferenceDocumentUpdate
{
    [Table("StandardUpdateStatus")]
    public class StandardUpdateStatus
    {
        [Key]
        public int Id { get; set; }

        [Column("StandardUpdateStatus")]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Column("StandardUpdateStatusFR")]
        [MaxLength(100)]
        public string? NameFR { get; set; }

        public int SortOrder { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public bool IsRetired { get; set; }

        public DateTime? RetiredOn { get; set; }

        // Navigation property
        public ICollection<StandardUpdateSubStatus>? SubStatuses { get; set; }
    }
}
