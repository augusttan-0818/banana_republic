using AutoMapper;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesTasks;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesTasks;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.CodesTaskActivities;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.StrategicPriority;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.Dependencies;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.TaskType;

namespace NRC.Const.CodesAPI.API.Profiles
{
    public class CodesTasksProfile : Profile
    {
        public CodesTasksProfile()
        {
            //TASK---------------------------------------------------
            //CREATE
            CreateMap<CodesTask, CodesTasksCreateRequest>();
            CreateMap<CodesTasksCreateRequest, CodesTask>();

            CreateMap<CodesTasksCreateRequest, CreateTask_Request>();
            CreateMap<CreateTask_Request, CodesTasksCreateRequest>();

            CreateMap<CreateTask_Result, CodesTask>();
            CreateMap<CodesTask, CreateTask_Result>();

            CreateMap<CreateTask_Response, CreateTask_Result>();
            CreateMap<CreateTask_Result, CreateTask_Response>();

            CreateMap<CodesTasksCreateRequest, CreateTask_Result>();

            //READ
            CreateMap<GetTasks_Response, GetTasks_Result>();
            CreateMap<GetTasks_Result, GetTasks_Response>();

            //READ SINGLE
            CreateMap<GetSingleTask_Response, GetSingleTask_Result>();
            CreateMap<GetSingleTask_Result, GetSingleTask_Response>();

            //UPDATE
            CreateMap<CodesTask, UpdateCodesTasks_Result>();
            CreateMap<UpdateCodesTasks_Result, UpdateTask_Response>();
            CreateMap<UpdateCodesTasks_Result, CodesTask>();
            CreateMap<UpdateTask_Request, CodesTasksUpdateRequest>();
            CreateMap<CodesTasksUpdateRequest, CodesTask>();
            CreateMap<CodesTask, CodesTasksUpdateRequest>();

            //DELETE (optional, can use workaround)
            CreateMap<GetSingleTask_Result, CodesTask>();
            CreateMap<CodesTask, GetSingleTask_Result>();
            //-------------------------------------------------------

            //ACTIVITY-----------------------------------------------

            //READ
            CreateMap<GetTaskActivity_Response, GetTasksActivity_Result>();
            CreateMap<GetTasksActivity_Result, GetTaskActivity_Response>();

            //READ SINGLE
            CreateMap<GetSingleTaskActivity_Response, GetSingleTasksActivity_Result>();
            CreateMap<GetSingleTasksActivity_Result, GetSingleTaskActivity_Response>();

            //CREATE
            CreateMap<CodesTaskActivities, CodesTasksActivityCreateRequest>();
            CreateMap<CodesTasksActivityCreateRequest, CodesTaskActivities>();

            CreateMap<CodesTasksActivityCreateRequest, CreateTaskActivity_Request>();
            CreateMap<CreateTaskActivity_Request, CodesTasksActivityCreateRequest>();

            CreateMap<CreateActivity_Result, CodesTaskActivities>();
            CreateMap<CodesTaskActivities, CreateActivity_Result>();

            CreateMap<CreateTaskActivity_Response, CreateActivity_Result>();
            CreateMap<CreateActivity_Result, CreateTaskActivity_Response>();

            CreateMap<CodesTasksActivityCreateRequest, CreateActivity_Result>();

            //UPDATE
            CreateMap<CodesTaskActivities, UpdateCodesTasksActivity_Result>();
            CreateMap<UpdateCodesTasksActivity_Result, UpdateTaskActivity_Response>();
            CreateMap<UpdateCodesTasksActivity_Result, CodesTaskActivities>();
            CreateMap<UpdateTaskActivity_Request, CodesTasksActivityUpdateRequest>();
            //-------------------------------------------------------

            //STRATEGIC PRIORITY---------------------------------------
            //READ
            CreateMap<GetStrategicPriority_Response, GetStrategicPriorities_Result>();
            CreateMap<GetStrategicPriorities_Result, GetStrategicPriority_Response>();

            //READ SINGLE
            CreateMap<GetSingleStrategicPriority_Response, GetSingleStrategicPriority_Result>();
            CreateMap<GetSingleStrategicPriority_Result, GetSingleStrategicPriority_Response>();
            //---------------------------------------------------------

            //DEPENDENCIES---------------------------------------
            //READ
            CreateMap<GetDependencies_Response, GetDependencies_Result>();
            CreateMap<GetDependencies_Result, GetDependencies_Response>();

            //READ SINGLE
            CreateMap<GetSingleDependency_Response, GetSingleDependency_Result>();
            CreateMap<GetSingleDependency_Result, GetSingleDependency_Response>();

            //CREATE
            CreateMap<CodesTaskDependencies, CodesTasksDependencyCreateRequest>();
            CreateMap<CodesTasksDependencyCreateRequest, CodesTaskDependencies>();

            CreateMap<CodesTasksDependencyCreateRequest, CreateDependency_Request>();
            CreateMap<CreateDependency_Request, CodesTasksDependencyCreateRequest>();

            CreateMap<CreateDependency_Result, CodesTaskDependencies>();
            CreateMap<CodesTaskDependencies, CreateDependency_Result>();

            CreateMap<CreateDependency_Response, CreateDependency_Result>();
            CreateMap<CreateDependency_Result, CreateDependency_Response>();

            CreateMap<CodesTasksDependencyCreateRequest, CreateDependency_Result>();

            //UPDATE
            CreateMap<CodesTaskDependencies, UpdateDependency_Result>();
            CreateMap<UpdateDependency_Result, UpdateDependency_Response>();
            CreateMap<UpdateDependency_Result, CodesTaskDependencies>();
            CreateMap<UpdateDependency_Request, CodesTasksDependencyUpdateRequest>();

            //DELETE (optional, can use workaround)
            CreateMap<GetSingleDependency_Result, CodesTaskDependencies>();
            CreateMap<CodesTaskDependencies, GetSingleDependency_Result>();
            //---------------------------------------------------------

            //TASK TYPES ----------------------------------------------
            //READ ----------------------------------------------------
            CreateMap<GetTaskType_Response, GetTaskType_Result>();
            CreateMap<GetTaskType_Result, GetTaskType_Response>();
            //---------------------------------------------------------

            CreateMap<CodesTasksCreateRequest, CodesTask>()
                .ForMember(d => d.CodesCycleId, o => o.MapFrom(s => s.CodesCycleId));

            CreateMap<CodesTasksUpdateRequest, CodesTask>()
                .ForMember(d => d.CodesCycleId, o => o.MapFrom(s => s.CodesCycleId));

            CreateMap<CodesTask, GetTasks_Result>()
                .ForMember(d => d.CodesCycleId, o => o.MapFrom(s => s.CodesCycleId));

            CreateMap<CodesTask, GetSingleTask_Result>()
                .ForMember(d => d.CodesCycleId, o => o.MapFrom(s => s.CodesCycleId));
        }
    }
}

