using AutoMapper;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Committees;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Committees;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Committees;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Committees;



namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesCommitteeProfile: Profile
    {
        public CodesCommitteeProfile() {
            CreateMap<CodesCommittee, CommitteeCreateRequest>();
            CreateMap<CreateCommittee_Result, CodesCommittee>();
            CreateMap<CodesCommittee, CreateCommittee_Result>();
            CreateMap<CodesCommittee, UpdateCommittee_Result>();
            CreateMap<GetCommitteeMinimal_Result, CodesCommittee>();
            CreateMap<GetCommittees_Response, GetCommittees_Result>();
            CreateMap<GetCommittees_Result, GetCommittees_Response>();
            CreateMap<GetSingleCommittee_Response, GetSingleCommittee_Result>();
            CreateMap<GetSingleCommittee_Result, GetSingleCommittee_Response>();
            CreateMap<GetParentCommittees_Result, GetParentCommittees_Response >();
            CreateMap<CommitteeCreateRequest, CreateCommittee_Request>();
          
            CreateMap<CreateCommittee_Response, CreateCommittee_Result>();
            CreateMap<CreateCommittee_Request, CommitteeCreateRequest>();
            CreateMap<CommitteeCreateRequest, CodesCommittee>();
            CreateMap<CodesCommittee, CreateCommittee_Result>();
            CreateMap<CreateCommittee_Result, CodesCommittee>();
            CreateMap<CommitteeCreateRequest, CreateCommittee_Result>();
            CreateMap<CreateCommittee_Result, CreateCommittee_Response>();

            CreateMap<UpdateCommittee_Result, UpdateCommittee_Response>();
            CreateMap<UpdateCommittee_Result, CodesCommittee>();
            CreateMap<UpdateCommittee_Request, CommitteeUpdateRequest>();

        }
    }
}

