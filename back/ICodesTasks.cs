using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesTasks;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesTasks;

namespace NRC.Const.CodesAPI.Application.Interfaces
{
    public interface ICodesTasks
    {

        Task<CreateTask_Result> CreateTaskAsync(CodesTasksCreateRequest reqTask);
        Task<IEnumerable<GetTasks_Result>> GetTasksAsync();

        Task<GetSingleTask_Result?> GetTaskByIdAsync(int id);

        Task DeleteTaskAsync(long id);

        Task<UpdateCodesTasks_Result> UpdateTaskAsync(CodesTasksUpdateRequest reqTask);

        Task<IEnumerable<GetTasksActivity_Result>> GetActivitiesAsync();

        Task<GetSingleTasksActivity_Result?> GetActivityByIdAsync(int id);

        Task DeleteActivityAsync(int id);

        Task<CreateActivity_Result> CreateActivityAsync(CodesTasksActivityCreateRequest reqActivity);

        Task<UpdateCodesTasksActivity_Result> UpdateActivityAsync(CodesTasksActivityUpdateRequest reqActivity);

        Task<IEnumerable<GetStrategicPriorities_Result>> GetStrategicPrioritiesAsync();

        Task<GetSingleStrategicPriority_Result?> GetStrategicPriorityByIdAsync(int id);

        Task<IEnumerable<GetDependencies_Result>> GetDependenciesAsync();

        Task<IEnumerable<GetDependencies_Result>> GetDependenciesBySourceTaskAsync(long sourceTask);

        Task<IEnumerable<GetDependencies_Result>> GetDependenciesBySourceActivityAsync(int sourceActivity);

        Task<GetSingleDependency_Result?> GetDependencyByIdAsync(int id);

        Task<IEnumerable<GetSingleDependency_Result>?> GetMultipleDependenciesByIdAsync(IEnumerable<long> ids);

        Task<CreateDependency_Result> CreateDependencyAsync(CodesTasksDependencyCreateRequest reqTask);

        Task DeleteDependencyAsync(long id);

        Task DeleteMultipleDependenciesAsync(IEnumerable<long> ids);

        Task<UpdateDependency_Result> UpdateDependencyAsync(CodesTasksDependencyUpdateRequest reqTask);

        Task<IEnumerable<GetTaskType_Result>> GetTaskTypesAsync();

        Task<IEnumerable<CreateDependency_Result>> CreateMultipleDependenciesAsync(IEnumerable<CodesTasksDependencyCreateRequest> reqDependencies);

        Task<IEnumerable<UpdateDependency_Result>> UpdateMultipleDependenciesAsync(IEnumerable<CodesTasksDependencyUpdateRequest> reqDependencies);

    }
}

