
using System.ComponentModel.DataAnnotations;

namespace NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests
{
    public class CodesCCRStatus
    {
        [Key]
        public short StatusId { get; set; }

        [Required, MaxLength(50)]
        public string StatusCode { get; set; } = null!;

        [Required, MaxLength(100)]
        public string StatusName { get; set; } = null!;

        [MaxLength(100)]
        public string? StatusNameFR { get; set; }
    }

}
