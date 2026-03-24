using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.PublicReviewComments;
using NRC.Const.CodesAPI.Domain.Entities.PublicReviewComments;
namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface IPublicReviewCommentRepository
    {
        Task<PagedResult<CodesPublicReviewCommentsUnified>> SearchAsync(PublicReviewCommentSearchRequest request, int pageNumber, int pageSize);
        Task<CodesPublicReviewCommentsUnified> GetPublicReviewCommentByIdAsync(int id);
        Task <CodesPublicReviewCommentsUnified> UpdatePublicReviewComment(PublicReviewCommentUpdateRequest updateRequest);
    }
}