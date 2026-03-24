using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Users;

namespace NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodeResources
{
    public class GetCodesResourcesResponse
    {
        public int ResourceId { get; set; }
        public long? ResourceUserId { get; set; }
        public short CodesCycleId { get; set; }
        public GetUserResponseWithoutCodesResource? User { get; set; }
    }

    public class GetCodesResourcesResponseWithoutUser
    {
        public int ResourceId { get; set; }
        public long? ResourceUserId { get; set; }
        public short CodesCycleId { get; set; }
        
    }


}

