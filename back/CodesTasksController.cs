using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.CodesTaskActivities;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.Dependencies;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.StrategicPriority;
using NRC.Const.CodesAPI.Application.DTOs.AppDTOs.CodesTasks.TaskType;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesTasks;
using NRC.Const.CodesAPI.Application.Interfaces;

namespace NRC.Const.CodesAPI.API.Controllers
{
    [Route("api/v{version:apiVersion}/codestasks/")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize(Policy = AuthorizationPolicyPrefixes.RoleAny + AuthorizationPolicyRoles.Viewer)]
    public class CodesTasksController(ICodesTasks workPlanRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICodesTasks _codesTasksRepository = workPlanRepository ?? throw new ArgumentNullException(nameof(workPlanRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetTasks_Response>>> GetTasks()
        {
            var taskEntities = await _codesTasksRepository.GetTasksAsync();
            return Ok(_mapper.Map<IEnumerable<GetTasks_Response>>(taskEntities));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSingleTask_Response>> GetTask(int id)
        {
            var task = await _codesTasksRepository.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return Ok(_mapper.Map<GetSingleTask_Response>(task));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<CreateTask_Response>> CreateTask([FromBody] CreateTask_Request newTask)
        {
            if (newTask == null)
            {
                return BadRequest("Task data is required.");
            }
            try
            {
                var task = await _codesTasksRepository.CreateTaskAsync(_mapper.Map<CodesTasksCreateRequest>(newTask));
                return CreatedAtAction(nameof(GetTasks), new { id = task.TaskId }, _mapper.Map<CreateTask_Response>(task));
            }
            catch (InvalidOperationException ex)
            {

                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    // If there is an inner exception, log or print its message
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);

                    // Optionally, you can also print the inner exception's stack trace
                    Console.WriteLine("Inner exception stack trace: " + ex.InnerException.StackTrace);
                }
                else
                {
                    Console.WriteLine("No inner exception.");
                }
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UpdateTask_Response>> PutTask(int id, UpdateTask_Request task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }

            try
            {
                var gettaskresult = await _codesTasksRepository.UpdateTaskAsync(_mapper.Map<CodesTasksUpdateRequest>(task));
                return Ok(_mapper.Map<UpdateTask_Response>(gettaskresult));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _codesTasksRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await _codesTasksRepository.DeleteTaskAsync(id);

            return NoContent();
        }

        [HttpGet("activities")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetTaskActivity_Response>>> GetActivities()
        {
            var taskActivityEntities = await _codesTasksRepository.GetActivitiesAsync();
            return Ok(_mapper.Map<IEnumerable<GetTaskActivity_Response>>(taskActivityEntities));
        }


        [HttpGet("activities/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSingleTaskActivity_Response>> GetActivity(int id)
        {
            var activity = await _codesTasksRepository.GetActivityByIdAsync(id);

            if (activity == null)
            {
                return NotFound($"Activity with ID {id} not found.");
            }

            return Ok(_mapper.Map<GetSingleTaskActivity_Response>(activity));
        }

        [HttpDelete("activities/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _codesTasksRepository.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            await _codesTasksRepository.DeleteActivityAsync(id);

            return NoContent();
        }

        [HttpPost("activities")]
        [Produces("application/json")]
        public async Task<ActionResult<CreateTaskActivity_Response>> CreateActivity([FromBody] CreateTaskActivity_Request newActivity)
        {
            if (newActivity == null)
            {
                return BadRequest("Task data is required.");
            }
            try
            {
                var activity = await _codesTasksRepository.CreateActivityAsync(_mapper.Map<CodesTasksActivityCreateRequest>(newActivity));
                return CreatedAtAction(nameof(GetActivities), new { id = activity.TaskActivityId }, _mapper.Map<CreateTaskActivity_Response>(activity));
            }
            catch (InvalidOperationException ex)
            {

                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    // If there is an inner exception, log or print its message
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);

                    // Optionally, you can also print the inner exception's stack trace
                    Console.WriteLine("Inner exception stack trace: " + ex.InnerException.StackTrace);
                }
                else
                {
                    Console.WriteLine("No inner exception.");
                }
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPut("activities/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UpdateTaskActivity_Response>> PutActivity(int id, UpdateTaskActivity_Request activity)
        {
            if (id != activity.TaskActivityId)
            {
                return BadRequest();
            }

            try
            {
                var getactivityresult = await _codesTasksRepository.UpdateActivityAsync(_mapper.Map<CodesTasksActivityUpdateRequest>(activity));
                return Ok(_mapper.Map<UpdateTaskActivity_Response>(getactivityresult));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", detail = ex.Message });
            }
        }

        [HttpGet("strategicpriorities")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetStrategicPriority_Response>>> GetStrategicPriorities()
        {
            var strategicpriorityEntities = await _codesTasksRepository.GetStrategicPrioritiesAsync();
            return Ok(_mapper.Map<IEnumerable<GetStrategicPriority_Response>>(strategicpriorityEntities));
        }

        [HttpGet("strategicpriorities/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSingleStrategicPriority_Response>> GetStrategicPriority(int id)
        {
            var strategicPriority = await _codesTasksRepository.GetStrategicPriorityByIdAsync(id);

            if (strategicPriority == null)
            {
                return NotFound($"Strategic Priority with ID {id} not found.");
            }

            return Ok(_mapper.Map<GetSingleStrategicPriority_Response>(strategicPriority));
        }

        [HttpGet("dependencies")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetDependencies_Response>>> GetDependencies()
        {
            var dependencyEntities = await _codesTasksRepository.GetDependenciesAsync();
            return Ok(_mapper.Map<IEnumerable<GetDependencies_Response>>(dependencyEntities));
        }

        [HttpGet("dependencies/sourceTask/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetDependencies_Response>>> GetDependenciesBySourceTask(long id)
        {
            var dependencyEntities = await _codesTasksRepository.GetDependenciesBySourceTaskAsync(id);
            return Ok(_mapper.Map<IEnumerable<GetDependencies_Response>>(dependencyEntities));
        }

        [HttpGet("dependencies/sourceActivity/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetDependencies_Response>>> GetDependencyBySourceActivity(int id)
        {
            var dependencyEntities = await _codesTasksRepository.GetDependenciesBySourceActivityAsync(id);
            return Ok(_mapper.Map<IEnumerable<GetDependencies_Response>>(dependencyEntities));
        }

        [HttpGet("dependencies/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetSingleDependency_Response>> GetDependency(int id)
        {
            var dependency = await _codesTasksRepository.GetDependencyByIdAsync(id);

            if (dependency == null)
            {
                return NotFound($"Strategic Priority with ID {id} not found.");
            }

            return Ok(_mapper.Map<GetSingleDependency_Response>(dependency));
        }

        [HttpPost("dependencies")]
        [Produces("application/json")]
        public async Task<ActionResult<CreateDependency_Response>> CreateDependency([FromBody] CreateDependency_Request newDependency)
        {
            if (newDependency == null)
            {
                return BadRequest("Target dependency not found");
            }
            try
            {
                var dependency = await _codesTasksRepository.CreateDependencyAsync(_mapper.Map<CodesTasksDependencyCreateRequest>(newDependency));
                return CreatedAtAction(nameof(GetDependencies), new { id = dependency.DependencyId }, _mapper.Map<CreateDependency_Response>(dependency));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    // If there is an inner exception, log or print its message
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);

                    // Optionally, you can also print the inner exception's stack trace
                    Console.WriteLine("Inner exception stack trace: " + ex.InnerException.StackTrace);
                }
                else
                {
                    Console.WriteLine("No inner exception.");
                }
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPut("dependencies/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UpdateDependency_Response>> PutDependency(int id, UpdateDependency_Request dependency)
        {
            if (id != dependency.DependencyId)
            {
                return BadRequest();
            }

            try
            {
                var getdependencyresult = await _codesTasksRepository.UpdateDependencyAsync(_mapper.Map<CodesTasksDependencyUpdateRequest>(dependency));
                return Ok(_mapper.Map<UpdateDependency_Response>(getdependencyresult));
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing the request.", detail = ex.Message });
            }
        }

        [HttpGet("tasktypes")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<GetTaskType_Response>>> GetTaskType()
        {
            var taskTypeEntities = await _codesTasksRepository.GetTaskTypesAsync();
            return Ok(_mapper.Map<IEnumerable<GetTaskType_Response>>(taskTypeEntities));
        }

        [HttpPost("dependencies/multiple")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<CreateDependency_Response>>> CreateMultipleDependencies([FromBody] List<CreateDependency_Request> newDependencies)
        {
            if (newDependencies == null || !newDependencies.Any())
            {
                return BadRequest("No dependencies provided.");
            }

            try
            {
                var reqList = _mapper.Map<List<CodesTasksDependencyCreateRequest>>(newDependencies);
                var createdDependencies = await _codesTasksRepository.CreateMultipleDependenciesAsync(reqList);
                var response = _mapper.Map<List<CreateDependency_Response>>(createdDependencies);
                return CreatedAtAction(nameof(GetDependencies), null, response);

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception stack trace: " + ex.InnerException.StackTrace);
                }
                else
                {
                    Console.WriteLine("No inner exception.");
                }
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpPut("dependencies/multiple")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<UpdateDependency_Response>>> UpdateMultipleDependencies([FromBody] List<UpdateDependency_Request> dependencies)
        {
            if (dependencies == null || !dependencies.Any())
            {
                return BadRequest("No dependencies provided.");
            }

            try
            {
                var reqList = _mapper.Map<List<CodesTasksDependencyUpdateRequest>>(dependencies);
                var updatedDependencies = await _codesTasksRepository.UpdateMultipleDependenciesAsync(reqList);
                var response = _mapper.Map<List<UpdateDependency_Response>>(updatedDependencies);
                return CreatedAtAction(nameof(GetDependencies), null, response);

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception stack trace: " + ex.InnerException.StackTrace);
                }
                else
                {
                    Console.WriteLine("No inner exception.");
                }
                return StatusCode(500, new { message = "An error occurred while processing the request.", details = ex.Message });
            }
        }

        [HttpDelete("dependencies/multiple")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteMultipleDependencies(List<long> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("No dependency IDs provided.");
            }

            var dependencies = await _codesTasksRepository.GetMultipleDependenciesByIdAsync(ids);

            if (dependencies == null || !dependencies.Any())
            {
                return NotFound("Dependencies not found.");
            }

            // Call DeleteMultipleDependenciesAsync with the list of IDs, not the dependencies
            await _codesTasksRepository.DeleteMultipleDependenciesAsync(ids);

            return NoContent();
        }


    }
}
