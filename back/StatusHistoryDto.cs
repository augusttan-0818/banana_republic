namespace NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.CodeChangeRequests
{
    public class StatusHistoryDto
    {
        public long StatusHistoryId { get; set; }
        public short StatusId { get; set; }
        public DateTime ChangedDate { get; set; }
        public string? Comments { get; set; }
    }

}
