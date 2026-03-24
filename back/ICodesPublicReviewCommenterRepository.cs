using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesPublicReviewCommenters;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesPublicReviewCommenterRepository
    {
        Task<IEnumerable<GetCodesPublicReviewCommenter_Result>> GetCodesPublicReviewCommentersAsync();

        Task<GetCodesPublicReviewCommenter_Result?> GetCodesPublicReviewCommenterByIdAsync(int id);
    }
}

