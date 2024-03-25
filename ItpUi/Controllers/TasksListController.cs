using ItpBusiness.Services;

using Microsoft.AspNetCore.Mvc;

namespace Itp.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TasksListController: ControllerBase
    {
        private readonly TasksService _tasksService;
        private readonly ProjectsService _projectsService;

        public TasksListController( 
            TasksService tasksService,
            ProjectsService projectsService)
        {     
            _projectsService = projectsService;
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<object> Get(string? projectId, string? projectDate)
        {
            await Task.CompletedTask;
            var tasks = String.IsNullOrWhiteSpace(projectId) ? 
                (await _tasksService.GetTasksAsync()) : 
                (await _tasksService.GetTasksAsync(projectId));
            if (String.IsNullOrWhiteSpace(projectDate) == false && projectDate != "0")
            {
                DateTime dateTime = DateTime.Parse(projectDate);            
                tasks = tasks.Where(task => $"{dateTime.Day}.{dateTime.Month}.{dateTime.Year}"== $"{task.CreateDate.Day}.{task.CreateDate.Month}.{task.CreateDate.Year}" ).ToList();
            }

            int position = 1;            
            var result = new
            {            
                Projects = (_projectsService.GetProjects()).Select(project => {
                    project.Tasks = new List<ItpDal.Entities.Task>();
                    return project;
                }),
                Tasks = tasks.Select(task => new {
                    Position = position++,
                    ProjectName = task?.Project?.ProjectName,
                    ProjectId = task?.Project?.Id,
                    TaskId = task?.Id,
                    TaskName = task?.TaskName,
                    StartTime = task?.StartDate,
                    EndTime = task?.EndDate,
                    Time = task?.StartDate == null? 0:
                    (
                        new DateTimeOffset((task.EndDate==null? DateTime.Now: (DateTime)task.EndDate)).ToUnixTimeSeconds() -
                        new DateTimeOffset(task.StartDate).ToUnixTimeSeconds()
                    )
                })
            };
            await Task.CompletedTask;
            return result;
        }
         
        [HttpPost]
        public async Task<object> Post([FromBody] PostTaskModel model)
        {
            var target = new ItpDal.Entities.Task()
            {
                TaskName = model.TaskName,
                ProjectId = new Guid(model.ProjectId),
                CreateDate = DateTime.Now,
                TaskDescription = model.TaskDescription,
            };
            if(String.IsNullOrWhiteSpace(model.StartTime)==false)
            {
                var startHours = int.Parse(model.StartTime.Substring(0, 2));
                var startMinutes = int.Parse(model.StartTime.Substring(3, 2));
                target.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHours, startMinutes, 0);
            }
            if (String.IsNullOrWhiteSpace(model.EndTime) == false)
            {
                var endHours = int.Parse(model.EndTime.Substring(0, 2));
                var endMinutes = int.Parse(model.EndTime.Substring(3, 2));
                target.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, endHours, endMinutes, 0);
            }
            var guid = await this._tasksService.CreateTaskAsync(target);            
            return new
            {
                ProjectId = target.ProjectId,
                Time = new DateTimeOffset((target.EndDate == null ? DateTime.Now : (DateTime)target.EndDate)).ToUnixTimeSeconds() -
                    new DateTimeOffset(target.StartDate).ToUnixTimeSeconds(),
                TaskId = guid.ToString(),
                TaskName = target.TaskName,
                StartTime = target?.StartDate,
                EndTime = target?.EndDate,
            };
        }
        public class PostTaskModel
        {
            public string TaskName { get; set; }
            public string ProjectId { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string TaskDescription { get; set; }
        }
    }
}
