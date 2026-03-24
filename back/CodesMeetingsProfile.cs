using AutoMapper;
using NRC.Const.CodesAPI.Domain.Entities.PublicReviews;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Meetings;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.Meetings;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.Meetings;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.Meetings;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesMeetingsProfile : Profile
    {
        public CodesMeetingsProfile()
        {
            CreateMap<GetMeetings_Response, GetMeetings_Result>(); 
            CreateMap<GetMeetings_Result, GetMeetings_Response>();

            CreateMap<CodesMeeting, MeetingsCreateRequest>();
            CreateMap<MeetingsCreateRequest, CodesMeeting>();

            CreateMap<MeetingsCreateRequest, CreateMeetings_Request>();
            CreateMap<CreateMeetings_Request, MeetingsCreateRequest>();

            CreateMap<CreateMeetings_Result, CodesMeeting>();
            CreateMap<CodesMeeting, CreateMeetings_Result>();

            CreateMap<CreateMeetings_Response, CreateMeetings_Result>();
            CreateMap<CreateMeetings_Result, CreateMeetings_Response>();

            CreateMap<MeetingsCreateRequest, CreateMeetings_Result>();

            CreateMap<CodesMeeting, UpdateMeetings_Result>();
            CreateMap<UpdateMeetings_Result, UpdateMeetings_Response>();
            CreateMap<UpdateMeetings_Result, CodesMeeting>();
            CreateMap<UpdateMeetings_Request, MeetingsUpdateRequest>();

            CreateMap<GetSingleMeeting_Response, GetSingleMeeting_Result>();
            CreateMap<GetSingleMeeting_Result, GetSingleMeeting_Response>();

            CreateMap<GetSingleMeeting_Result, CodesMeeting>();
            CreateMap<CodesMeeting, GetSingleMeeting_Result>();
        }


    }
}

