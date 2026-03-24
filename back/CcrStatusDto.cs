
namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests
{
    public class CcrStatusDto
    {
        public long StatusHistoryId { get; set; }
        public int CCRId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime ChangedDate { get; set; }
        public string ChangedBy { get; set; }
        public string? Comments { get; set; }
    }
}
