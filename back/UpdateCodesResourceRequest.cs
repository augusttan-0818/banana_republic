namespace NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodeResources
{
    public class UpdateCodesResourceRequest
    {
        public int ResourceId { get; set; }
        public long ResourceUserId { get; set; }
        public short CodesCycleId { get; set; }
    }
}

