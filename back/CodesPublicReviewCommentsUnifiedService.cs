using AutoMapper;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.PublicReviewComments;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.PublicReviewComments;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.Application.Services
{
    public class CodesPublicReviewCommentsUnifiedService : ICodesPublicCommentUnifiedService
    {
        private readonly IPublicReviewCommentRepository _repo;
        private readonly IMapper _mapper;

        public CodesPublicReviewCommentsUnifiedService(
            IPublicReviewCommentRepository repo,
            IMapper mapper
            )
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedResult<GetPublicReviewComments_Result>> Search(PublicReviewCommentSearchRequest request, int pageNumber, int pageSize)
        {
            try
            {
                var entities = await _repo.SearchAsync(request, pageNumber, pageSize);
                return _mapper.Map<PagedResult<GetPublicReviewComments_Result>>(entities);
            }
            catch(Exception e)
            {
                 throw new Exception(e.Message);
            }
        }

        public async Task<GetPublicReviewComments_Result?> GetPublicReviewCommentByIdAsync(int id)
        {
            var entity = await _repo.GetPublicReviewCommentByIdAsync(id);
            return entity == null ? null : _mapper.Map<GetPublicReviewComments_Result>(entity);
        }

        public async Task <UpdatePublicReviewComment_Result> UpdatePublicCommentAsync(PublicReviewCommentUpdateRequest request)
        {
           var entity = await _repo.UpdatePublicReviewComment(request);
           if(entity == null) return null;

           return _mapper.Map<UpdatePublicReviewComment_Result>(entity);

        }

    }
}
