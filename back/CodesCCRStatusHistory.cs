using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Resources;
using System.Text.Json.Serialization;

namespace NRC.Const.CodesAPI.Domain.Entities.CodeChangeRequests
{
    public class CodesCCRStatusHistory
    {
        public long StatusHistoryId { get; set; }

        public int CCRId { get; set; }
        public short StatusId { get; set; }
        public CodesCCRStatus Status { get; set; } = null!;

        public DateTime ChangedDate { get; set; }
        public int? ChangedById { get; set; }
        public CodesResource ChangedBy { get; set; }
        public string? Comments { get; set; }

        [JsonIgnore] 
        public CodesCCR CCR { get; set; } = null!;
    }

}
