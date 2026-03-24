////using System;
////using System.Threading.Tasks;
//using Application.Interfaces;

//namespace Application.Services.CodesTasks
//{
//    public class DependencyValidationService : IDependencyValidationService
//    {
//        private readonly ICodesTasks _taskRepository;
//        private readonly IDependenciesRepository _dependencyRepository;

//        public DependencyValidationService(
//            ICodesTaskRepository taskRepository,
//            ICodesTaskDependenciesRepository dependencyRepository)
//        {
//            _taskRepository = taskRepository;
//            _dependencyRepository = dependencyRepository;
//        }

//        public async Task ValidateDependenciesAsync(int taskId, DateTime proposedStart, DateTime proposedEnd)
//        {
//            var dependencies = await _dependencyRepository.GetDependenciesByTaskIdAsync(taskId);

//            foreach (var dep in dependencies)
//            {
//                var sourceTask = await _taskRepository.GetTaskByIdAsync(dep.SourceTaskId);

//                switch (dep.DependencyType)
//                {
//                    case "FS":
//                        if (sourceTask.EndDate > proposedStart)
//                            throw new InvalidOperationException($"Finish-to-Start violation: Task {taskId} cannot start before Task {sourceTask.TaskId} finishes.");
//                        break;

//                    case "SS":
//                        if (sourceTask.StartDate > proposedStart)
//                            throw new InvalidOperationException($"Start-to-Start violation: Task {taskId} cannot start before Task {sourceTask.TaskId} starts.");
//                        break;

//                    case "FF":
//                        if (sourceTask.EndDate > proposedEnd)
//                            throw new InvalidOperationException($"Finish-to-Finish violation: Task {taskId} cannot finish before Task {sourceTask.TaskId} finishes.");
//                        break;

//                    case "SF":
//                        if (sourceTask.StartDate > proposedEnd)
//                            throw new InvalidOperationException($"Start-to-Finish violation: Task {taskId} cannot finish before Task {sourceTask.TaskId} starts.");
//                        break;
//                }
//            }
//        }
//    }
//}
