using ItpBusiness.Services;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Text;

namespace Itp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditTaskController : ControllerBase
    {
        private readonly TasksService _tasksService;
        private readonly ProjectsService _projectsService;

        public EditTaskController(
            TasksService tasksService,
            ProjectsService projectsService)
        {
            _projectsService = projectsService;
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<object> Get(string taskId)
        {            
            var task = await this._tasksService.GetTaskAsync(taskId);            
            var result = new
            {
                TaskName = task.TaskName,
                ProjectId = task?.ProjectId,
                TaskDescription = task?.TaskDescription,
                ProjectName = task?.Project?.ProjectName,
                StartTime = $"{(task?.StartDate.Hour < 10 ? ("0" + task?.StartDate.Hour) : task?.StartDate.Hour)}:{(task?.StartDate.Minute < 10 ? ("0" + task?.StartDate.Minute) : task?.StartDate.Minute)}",
                EndTime =
                    task?.EndDate == null? "":
                    $"{(((DateTime)task?.EndDate).Hour < 10 ? ("0" + ((DateTime)task?.EndDate).Hour) : ((DateTime)task?.EndDate).Hour)}:{(((DateTime)task?.EndDate).Minute < 10 ? ("0" + ((DateTime)task?.EndDate).Minute) : ((DateTime)task?.EndDate).Minute)}",              
                TaskComments = task?.TaskComments.Select(comment => new
                {
                    Id = comment.Id,
                    TaskId = comment.TaskId,
                    CommentType = comment.CommentType,
                    Content = comment.CommentType == 1?
                        (object)Encoding.UTF8.GetString(comment.Content): (object)comment.Content
                }),
                Projects = (await _projectsService.GetProjectsAsync()).Select(project => {
                    project.Tasks = new List<ItpDal.Entities.Task>();
                    return project;
                }),
            };
            return result;
        }

        
        [HttpPost("PostFileComment")]
        public async Task<object> PostFileComment([FromQuery] string TaskId, [FromQuery] byte CommentType)
        {
            if (this.Request.Form.Files.Count < 1)
                throw new ArgumentNullException("Request body not contains file");

            using (var memoryStream = new MemoryStream())
            {
                this.Request.Form.Files[0].CopyTo(memoryStream);

                var content = memoryStream.ToArray();
                string id = await this._tasksService.AddFileCommentAsync(TaskId, CommentType, content);
                var result = new
                {
                    Result = 1,
                    Id = id
                };
                return await Task.FromResult(result);
            }        
        }

        [HttpPost("PostTextComment")]
        public async Task<object> PostTextComment([FromBody] PostCommentModel model)
        {
            string id = await this._tasksService.AddTextCommentAsync(model.TaskId, model.CommentType, model.Content);        
            return new
            {
                Result = 1,
                Id = id
            };
        }

        public class PostCommentModel
        {
            public string TaskId { get; set; }
            public byte CommentType { get; set; }
            public string Content { get; set; } 
        }

        [HttpDelete]
        public async Task<object> Delete(string commentId)                    
            => await _tasksService.RemoveCommentAsync(commentId);
        


        [HttpPatch]
        public async Task<int> Patch([FromBody] PatchTaskModel model)
        {
            return await _tasksService.UpdateTaskAsync(model.TaskId, model.ProjectId, model.TaskName, model.StartTime, model.EndTime, model.TaskDescription);
        }

        public class PatchTaskModel
        {
            public string TaskId { get; set; }
            public string TaskName { get; set; }
            public string ProjectId { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string TaskDescription { get; set; }
        }
    }
}
