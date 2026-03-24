using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.PublicReviews;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.PublicReviews;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public sealed record PublicReviewUpdateSnapshot(
           int PublicReviewId,
           DateOnly? OldPCFsDueDate,
           DateOnly?  NewPCFsDueDate,
           string? PublicReviewTitle,
           short? CodesCycleId
        );
    public interface IPublicReviewRepository
    {
        Task<IEnumerable<GetPublicReviewsPhase_Result>> GetPublicReviewPhaseAsync();
        Task<IEnumerable<GetPublicReviews_Result>> GetPublicReviewAsync();
        Task<GetSinglePublicReview_Result?> GetPublicReviewByIdAsync(int id);
        Task<CreatePublicReview_Result> CreatePublicReviewAsync(PublicReviewCreateRequest reqPublicReview);
        Task<(UpdatePublicReview_Result Result, PublicReviewUpdateSnapshot Snapshot)> UpdatePublicReviewAsync(PublicReviewUpdateRequest reqPublicReview, CancellationToken ct = default);
        Task DeletePublicReviewAsync(int id);
    }

    public interface IPublicReviewService
    {
        Task<UpdatePublicReview_Result> UpdatePublicReviewAsync(PublicReviewUpdateRequest reqPublicReview, CancellationToken ct = default);
    }
}

