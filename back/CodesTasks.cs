//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRC.Const.CodesAPI.Domain.Entities.Workplanning.Tasks;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesTasks;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.ResponseEntities.CodesTasks;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;

namespace NRC.Const.CodesAPI.Infrastructure.Services.Repositories
{
    public class CodesTasks(TasksDbContext context, IMapper mapper) : ICodesTasks
    {
        private readonly TasksDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        public async Task<CreateTask_Result> CreateTaskAsync(CodesTasksCreateRequest reqTask)
        {
            // Check if a task with the same name already exists
            var existingTask = await _context.CodesTask
                .FirstOrDefaultAsync(r => r.TaskName == reqTask.TaskName);

            if (existingTask != null)
            {
                throw new InvalidOperationException("A task with this name already exists.");
            }

            CodesTask target = _mapper.Map<CodesTask>(reqTask);
            _context.CodesTask.Add(target);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateTask_Result>(target);
        }

        public async Task<IEnumerable<GetTasks_Result>> GetTasksAsync()
        {
            var tasks = await _context.CodesTask
                .Include(c => c.CodesCycle)
                .Select(static c => new GetTasks_Result
                {
                    TaskId = c.TaskId,
                    TaskName = c.TaskName,
                    Description = c.Description,
                    CodesCycleId = c.CodesCycleId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    AssignedDate = c.AssignedDate,
                    CompletionDate = c.CompletionDate,
                    TaskType = c.TaskType,
                    NMCCId = c.NMCCId,
                    TaskGroupId = c.TaskGroupId,
                    StrategicPriorityId = c.StrategicPriorityId,
                    PercentComplete = c.PercentComplete,
                })
                .ToListAsync();

            return tasks;
        }

        public async Task<GetSingleTask_Result?> GetTaskByIdAsync(int id)
        {
            var task = await _context.CodesTask
              .Include(c => c.CodesCycle)

              .Select(static c => new GetSingleTask_Result
              {
                  TaskId = c.TaskId,
                  TaskName = c.TaskName,
                  Description = c.Description,
                  StartDate = c.StartDate,
                  EndDate = c.EndDate,
                  AssignedDate = c.AssignedDate,
                  CompletionDate = c.CompletionDate,
                  TaskType = c.TaskType,
                  NMCCId = c.NMCCId,
                  TaskGroupId = c.TaskGroupId,
                  StrategicPriorityId = c.StrategicPriorityId,
                  PercentComplete = c.PercentComplete,
                  CodesCycleId = c.CodesCycleId,
              })
              .FirstOrDefaultAsync(c => c.TaskId == id);
            return task;
        }

        public async Task<UpdateCodesTasks_Result> UpdateTaskAsync(CodesTasksUpdateRequest reqTask)
        {
            var existingTask = await _context.CodesTask
                                         .FirstOrDefaultAsync(r => r.TaskId == reqTask.TaskId);

            if (existingTask == null)
            {
                throw new KeyNotFoundException($"Task with ID {reqTask.TaskId} not found.");
            }

            //Update properties
            //Check if title is unique (optional)
            if (!string.IsNullOrEmpty(reqTask.TaskName))
            {
                bool titleExists = await _context.CodesTask
                    .AnyAsync(r => r.TaskName == reqTask.TaskName && r.TaskId != reqTask.TaskId);

                if (titleExists)
                {
                    throw new InvalidOperationException("The task name must be unique.");
                }

                existingTask.TaskName = reqTask.TaskName;
            }

            existingTask.Description = reqTask.Description;
            existingTask.StartDate = reqTask.StartDate;
            existingTask.EndDate = reqTask.EndDate;
            existingTask.AssignedDate = reqTask.AssignedDate;
            existingTask.CompletionDate = reqTask.CompletionDate;
            existingTask.TaskType = reqTask.TaskType;
            existingTask.NMCCId = reqTask.NMCCId;
            existingTask.TaskGroupId = reqTask.TaskGroupId;
            existingTask.StrategicPriorityId = reqTask.StrategicPriorityId;
            existingTask.PercentComplete = reqTask.PercentComplete;
            existingTask.CodesCycleId = reqTask.CodesCycleId;


            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateCodesTasks_Result>(existingTask);
        }

        //public async Task DeleteTaskAsync(int id)
        //{
        //    // Directly retrieve the entity by its ID
        //    var entity = await _context.CodesTask
        //        .FirstOrDefaultAsync(c => c.TaskId == id);

        //    if (entity != null)
        //    {
        //        _context.CodesTask.Remove(entity); // Remove the tracked entity
        //        await _context.SaveChangesAsync();
        //    }
        //}
        public async Task DeleteTaskAsync(long id)
        {
            // start a transaction to keep consistency
            await using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1) find activities for this task
                var activities = await _context.CodesTasksActivities
                    .Where(a => a.TaskId == id)
                    .ToListAsync();

                // 2) prepare nullable activity id list
                var activityIdsNullable = activities.Select(a => (int?)a.TaskActivityId).ToList();

                // 3) find all deps that reference these activities OR the task itself
                var deps = await _context.CodesTaskDependencies
                    .Where(d =>
                        activityIdsNullable.Contains(d.SourceActivity) ||
                        activityIdsNullable.Contains(d.DestinationActivity) ||
                        d.SourceTask == id ||
                        d.DestinationTask == id
                    )
                    .ToListAsync();

                // 4) remove dependencies then activities
                if (deps.Any()) _context.CodesTaskDependencies.RemoveRange(deps);
                if (activities.Any()) _context.CodesTasksActivities.RemoveRange(activities);

                // 5) remove the task
                var task = await _context.CodesTask.FirstOrDefaultAsync(t => t.TaskId == id);
                if (task != null) _context.CodesTask.Remove(task);

                // 6) commit
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
        public async Task<IEnumerable<GetTasksActivity_Result>> GetActivitiesAsync()
        {
            var activities = await _context.CodesTasksActivities
                .Include(c => c.CodesTasks)
                .Include(c => c.CodesTaskResource)
                .Select(static c => new GetTasksActivity_Result
                {
                    TaskActivityId = c.TaskActivityId,
                    TaskId = c.TaskId,
                    ResourceId = c.ResourceId,
                    ActivityTitle = c.ActivityTitle,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    PercentComplete = c.PercentComplete
                })
                .ToListAsync();

            return activities;
        }

        public async Task<GetSingleTasksActivity_Result?> GetActivityByIdAsync(int id)
        {
            var activity = await _context.CodesTasksActivities
                .Include(c => c.CodesTasks)
                .Include(c => c.CodesTaskResource)
                .Select(static c => new GetSingleTasksActivity_Result
                {
                    TaskActivityId = c.TaskActivityId,
                    TaskId = c.TaskId,
                    ResourceId = c.ResourceId,
                    ActivityTitle = c.ActivityTitle,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    PercentComplete = c.PercentComplete
                })
              .FirstOrDefaultAsync(c => c.TaskActivityId == id);
            return activity;
        }

        //public async Task DeleteActivityAsync(int id)
        //{
        //    var entity = await _context.CodesTasksActivities
        //        .FirstOrDefaultAsync(c => c.TaskActivityId == id);

        //    if (entity != null)
        //    {
        //        _context.CodesTasksActivities.Remove(entity);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task DeleteActivityAsync(int id)
        {
            await using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1) Remove dependencies linked to this activity
                var deps = await _context.CodesTaskDependencies
                    .Where(d => d.SourceActivity == id || d.DestinationActivity == id)
                    .ToListAsync();

                if (deps.Any())
                {
                    _context.CodesTaskDependencies.RemoveRange(deps);
                }

                // 2) Remove the activity itself
                var entity = await _context.CodesTasksActivities
                    .FirstOrDefaultAsync(c => c.TaskActivityId == id);

                if (entity != null)
                {
                    _context.CodesTasksActivities.Remove(entity);
                }

                // 3) Commit once
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw; // bubble the exception to the controller
            }
        }

        public async Task<CreateActivity_Result> CreateActivityAsync(CodesTasksActivityCreateRequest reqActivity)
        {
            // Check if a task with the same name already exists
            var existingActivity = await _context.CodesTasksActivities
                .FirstOrDefaultAsync(r => r.ActivityTitle == reqActivity.ActivityTitle);

            if (existingActivity != null)
            {
                throw new InvalidOperationException("An activity with this name already exists.");
            }

            CodesTaskActivities target = _mapper.Map<CodesTaskActivities>(reqActivity);
            _context.CodesTasksActivities.Add(target);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateActivity_Result>(target);
        }

        public async Task<UpdateCodesTasksActivity_Result> UpdateActivityAsync(CodesTasksActivityUpdateRequest reqActivity)
        {
            var existingActivity = await _context.CodesTasksActivities
                                         .FirstOrDefaultAsync(r => r.TaskActivityId == reqActivity.TaskActivityId);

            if (existingActivity == null)
            {
                throw new KeyNotFoundException($"Activity with ID {reqActivity.TaskActivityId} not found.");
            }

            //Update properties
            //Check if title is unique (optional)
            if (!string.IsNullOrEmpty(reqActivity.ActivityTitle))
            {
                bool titleExists = await _context.CodesTasksActivities
                    .AnyAsync(r => r.ActivityTitle == reqActivity.ActivityTitle && r.TaskActivityId != reqActivity.TaskActivityId);

                if (titleExists)
                {
                    throw new InvalidOperationException("The activity title must be unique.");
                }

                existingActivity.ActivityTitle = reqActivity.ActivityTitle;
            }

            existingActivity.ResourceId = reqActivity.ResourceId;
            existingActivity.ActivityTitle = reqActivity.ActivityTitle;
            existingActivity.Description = reqActivity.Description;
            existingActivity.StartDate = reqActivity.StartDate;
            existingActivity.EndDate = reqActivity.EndDate;
            existingActivity.PercentComplete = reqActivity.PercentComplete;
            existingActivity.TaskId = reqActivity.TaskId;

            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateCodesTasksActivity_Result>(existingActivity);
        }

        public async Task<IEnumerable<GetStrategicPriorities_Result>> GetStrategicPrioritiesAsync()
        {
            var tasks = await _context.CodesStrategicPriority
                .Include(c => c.CodesCycle)
                .Select(static c => new GetStrategicPriorities_Result
                {
                    StrategicPriorityId = c.StrategicPriorityId,
                    Name = c.Name,
                    Description = c.Description,
                    CodesCycleId = c.CodesCycleId,
                })
                .ToListAsync();

            return tasks;
        }

        public async Task<GetSingleStrategicPriority_Result?> GetStrategicPriorityByIdAsync(int id)
        {
            var strategicPriority = await _context.CodesStrategicPriority
              .Include(c => c.CodesCycle)

              .Select(static c => new GetSingleStrategicPriority_Result
              {
                  StrategicPriorityId = c.StrategicPriorityId,
                  Name = c.Name,
                  Description = c.Description,
                  CodesCycleId = c.CodesCycleId,
              })
              .FirstOrDefaultAsync(c => c.StrategicPriorityId == id);
            return strategicPriority;
        }

        public async Task<IEnumerable<GetDependencies_Result>> GetDependenciesAsync()
        {
            var dependencies = await _context.CodesTaskDependencies
                .Include(c => c.SourceTaskRef)
                .Include(c => c.SourceActivityRef)
                .Include(c => c.DestinationTaskRef)
                .Include(c => c.DestinationActivityRef)
                .Select(static c => new GetDependencies_Result
                {
                    DependencyId = c.DependencyId,
                    DependencyType = c.DependencyType,
                    SourceTask = c.SourceTask,
                    SourceActivity = c.SourceActivity,
                    DestinationTask = c.DestinationTask,
                    DestinationActivity = c.DestinationActivity
                    
                })
                .ToListAsync();

            return dependencies;
        }

        public async Task<GetSingleDependency_Result?> GetDependencyByIdAsync(int id)
        {
            var dependency = await _context.CodesTaskDependencies
              .Include(c => c.SourceTaskRef)
              .Include(c => c.SourceActivityRef)
              .Include(c => c.DestinationTaskRef)
              .Include(c => c.DestinationActivityRef)
              .Select(static c => new GetSingleDependency_Result
              {
                  DependencyId = c.DependencyId,
                  DependencyType = c.DependencyType,
                  SourceTask = c.SourceTask,
                  SourceActivity = c.SourceActivity,
                  DestinationTask = c.DestinationTask,
                  DestinationActivity = c.DestinationActivity
              })
              .FirstOrDefaultAsync(c => c.DependencyId == id);
            return dependency;
        }

        public async Task<IEnumerable<GetSingleDependency_Result>?> GetMultipleDependenciesByIdAsync(IEnumerable<long> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("No dependency IDs provided.");
            }

            var dependencies = await _context.CodesTaskDependencies
              .Include(c => c.SourceTaskRef)
              .Include(c => c.SourceActivityRef)
              .Include(c => c.DestinationTaskRef)
              .Include(c => c.DestinationActivityRef)
              .Where(c => ids.Contains(c.DependencyId)) // Filter based on IDs
              .Select(c => new GetSingleDependency_Result
              {
                  DependencyId = c.DependencyId,
                  DependencyType = c.DependencyType,
                  SourceTask = c.SourceTask,
                  SourceActivity = c.SourceActivity,
                  DestinationTask = c.DestinationTask,
                  DestinationActivity = c.DestinationActivity
              })
              .ToListAsync(); // Return as a list

            return dependencies;
        }

        public async Task DeleteDependencyAsync(long id)
        {
            var entity = await _context.CodesTaskDependencies
                .FirstOrDefaultAsync(c => c.DependencyId == id);

            if (entity != null)
            {
                _context.CodesTaskDependencies.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMultipleDependenciesAsync(IEnumerable<long> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentException("No dependency IDs provided.");
            }

            var entities = await _context.CodesTaskDependencies
                .Where(c => ids.Contains(c.DependencyId))
                .ToListAsync();

            if (!entities.Any())
            {
                throw new KeyNotFoundException("Dependencies not found.");
            }

            _context.CodesTaskDependencies.RemoveRange(entities);

            await _context.SaveChangesAsync();
        }


        public async Task<CreateDependency_Result> CreateDependencyAsync(CodesTasksDependencyCreateRequest reqDependency)
        {
            CodesTaskDependencies target = _mapper.Map<CodesTaskDependencies>(reqDependency);
            _context.CodesTaskDependencies.Add(target);
            await _context.SaveChangesAsync();
            return _mapper.Map<CreateDependency_Result>(target);
        }

        public async Task<UpdateDependency_Result> UpdateDependencyAsync(CodesTasksDependencyUpdateRequest reqDependency)
        {
            var existingDependency = await _context.CodesTaskDependencies
                                         .FirstOrDefaultAsync(r => r.DependencyId == reqDependency.DependencyId);

            if (existingDependency == null)
            {
                throw new KeyNotFoundException($"Dependency with ID {reqDependency.DependencyId} not found.");
            }


            existingDependency.DependencyType = reqDependency.DependencyType;
            existingDependency.DependencyId = reqDependency.DependencyId;
            existingDependency.SourceTask = reqDependency.SourceTask;
            existingDependency.SourceActivity = reqDependency.SourceActivity;
            existingDependency.DestinationTask = reqDependency.DestinationTask;
            existingDependency.DestinationActivity = reqDependency.DestinationActivity;

            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateDependency_Result>(existingDependency);
        }

        public async Task<IEnumerable<GetTaskType_Result>> GetTaskTypesAsync()
        {
            var tasktypes = await _context.CodesTaskType
                .Select(static c => new GetTaskType_Result
                {
                    CodesTaskTypeId = c.CodesTaskTypeId,
                    Name = c.Name,
                    Description = c.Description,

                })
                .ToListAsync();

            return tasktypes;
        }

        public async Task<IEnumerable<GetDependencies_Result>> GetDependenciesBySourceTaskAsync(long sourceTask)
        {
            var dependencies = await _context.CodesTaskDependencies
                .Where(c => c.SourceTask == sourceTask)
                .Include(c => c.SourceTaskRef)
                .Include(c => c.SourceActivityRef)
                .Include(c => c.DestinationTaskRef)
                .Include(c => c.DestinationActivityRef)
                .Select(static c => new GetDependencies_Result
                {
                    DependencyId = c.DependencyId,
                    DependencyType = c.DependencyType,
                    SourceTask = c.SourceTask,
                    SourceActivity = c.SourceActivity,
                    DestinationTask = c.DestinationTask,
                    DestinationActivity = c.DestinationActivity

                })
                .ToListAsync();

            return dependencies;
        }

        public async Task<IEnumerable<GetDependencies_Result>> GetDependenciesBySourceActivityAsync(int sourceActivity)
        {
            var dependencies = await _context.CodesTaskDependencies
                .Where(c => c.SourceActivity == sourceActivity)
                .Include(c => c.SourceTaskRef)
                .Include(c => c.SourceActivityRef)
                .Include(c => c.DestinationTaskRef)
                .Include(c => c.DestinationActivityRef)
                .Select(static c => new GetDependencies_Result
                {
                    DependencyId = c.DependencyId,
                    DependencyType = c.DependencyType,
                    SourceTask = c.SourceTask,
                    SourceActivity = c.SourceActivity,
                    DestinationTask = c.DestinationTask,
                    DestinationActivity = c.DestinationActivity

                })
                .ToListAsync();

            return dependencies;
        }

        public async Task<IEnumerable<CreateDependency_Result>> CreateMultipleDependenciesAsync(IEnumerable<CodesTasksDependencyCreateRequest> reqDependencies)
        {
            if (reqDependencies == null || !reqDependencies.Any())
            {
                throw new ArgumentException("No dependencies provided.");
            }

            var entities = _mapper.Map<List<CodesTaskDependencies>>(reqDependencies);

            await _context.CodesTaskDependencies.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return _mapper.Map<List<CreateDependency_Result>>(entities);
        }

        public async Task<IEnumerable<UpdateDependency_Result>> UpdateMultipleDependenciesAsync(IEnumerable<CodesTasksDependencyUpdateRequest> reqDependencies)
        {
            if (reqDependencies == null || !reqDependencies.Any())
            {
                throw new ArgumentException("No dependencies provided.");
            }

            var updateResults = new List<UpdateDependency_Result>();

            foreach (var reqDependency in reqDependencies)
            {
                var existingDependency = await _context.CodesTaskDependencies
                                             .FirstOrDefaultAsync(r => r.DependencyId == reqDependency.DependencyId);

                if (existingDependency == null)
                {
                    throw new KeyNotFoundException($"Dependency with ID {reqDependency.DependencyId} not found.");
                }

                // Update the fields
                existingDependency.DependencyType = reqDependency.DependencyType;
                existingDependency.SourceTask = reqDependency.SourceTask;
                existingDependency.SourceActivity = reqDependency.SourceActivity;
                existingDependency.DestinationTask = reqDependency.DestinationTask;
                existingDependency.DestinationActivity = reqDependency.DestinationActivity;

                // Map the updated entity to the result type
                updateResults.Add(_mapper.Map<UpdateDependency_Result>(existingDependency));
            }

            // Save all changes after processing all updates
            await _context.SaveChangesAsync();

            return updateResults;
        }

    }
}
