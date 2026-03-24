using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.PublicReviewComments;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.PublicReviewComments;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesPublicCommentUnifiedService
    {
        Task<PagedResult<GetPublicReviewComments_Result>> Search(PublicReviewCommentSearchRequest request, int pageNumber, int pageSize);
        Task<GetPublicReviewComments_Result?> GetPublicReviewCommentByIdAsync(int id);
        Task <UpdatePublicReviewComment_Result> UpdatePublicCommentAsync(PublicReviewCommentUpdateRequest request);
    }
}
