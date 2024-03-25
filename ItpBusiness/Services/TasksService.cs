
using ItpDal;
using ItpDal.Entities;

using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ItpBusiness.Services
{
    public class TasksService
    {
        private readonly ItpDbContext itpDbContext;

        public TasksService( ItpDbContext itpDbContext ) {
            this.itpDbContext = itpDbContext;
        }

        public IEnumerable<ItpDal.Entities.Task> GetTasks()
            => this.itpDbContext.Tasks.Include(task => task.Project).AsNoTracking().ToList();

        public async Task<IEnumerable<ItpDal.Entities.Task>> GetTasksAsync()
            => await this.itpDbContext.Tasks.Include(task => task.Project).AsNoTracking().ToListAsync();

        public ItpDal.Entities.Task GetTask(string Guid)
            => this.itpDbContext.Tasks.Include(task => task.Project).Include(task => task.TaskComments).First(task => task.Id == new Guid(Guid));

        public async Task<ItpDal.Entities.Task> GetTaskAsync(string Guid)
            => await this.itpDbContext.Tasks.Include(task => task.Project).Include(task => task.TaskComments).FirstAsync(task => task.Id == new Guid(Guid));

        public IEnumerable<ItpDal.Entities.Task> GetTasks(string projectId)
            => this.itpDbContext.Tasks.Include(task => task.Project).Where(task => task.ProjectId == new Guid(projectId)).AsNoTracking().ToList();

        public async Task<IEnumerable<ItpDal.Entities.Task>> GetTasksAsync(string projectId)
            => await this.itpDbContext.Tasks.Include(task => task.Project).Where(task => task.ProjectId == new Guid(projectId)).AsNoTracking().ToListAsync();

        public Guid CreateTask(ItpDal.Entities.Task target)
        {
            try
            {
                target.Id = Guid.NewGuid();
                this.itpDbContext.Tasks.Add(target);
                if (this.itpDbContext.SaveChanges() == 1)
                {
                    return target.Id;
                }
                else
                {
                    return Guid.Empty;
                }
            }
            catch( DbUpdateException ex )
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public async Task<Guid> CreateTaskAsync(ItpDal.Entities.Task target)
        {
            target.Id = Guid.NewGuid();
            await this.itpDbContext.Tasks.AddAsync(target);
            if ((await this.itpDbContext.SaveChangesAsync()) == 1)
            {
                return target.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public string AddTextComment(string taskId, byte commentType, string content)
        {
            var target = new TaskComments()
            {
                Id = Guid.NewGuid(),
                TaskId = new Guid(taskId),
                CommentType = commentType,
                Content = Encoding.UTF8.GetBytes(content)
            };
            this.itpDbContext.TaskComments.Add(target);
            this.itpDbContext.SaveChanges();
            return target.Id.ToString();
        }

        public async Task<string> AddTextCommentAsync(string taskId, byte commentType, string content)
        {
            var target = new TaskComments()
            {
                Id = Guid.NewGuid(),
                TaskId = new Guid(taskId),
                CommentType = commentType,
                Content = Encoding.UTF8.GetBytes(content)
            };
            await this.itpDbContext.TaskComments.AddAsync(target);
            await this.itpDbContext.SaveChangesAsync();
            return target.Id.ToString();
        }

        public string AddFileComment(string taskId, byte commentType, byte[] bytes)
        {
            var target = new TaskComments()
            {
                Id = Guid.NewGuid(),
                TaskId = new Guid(taskId),
                CommentType = commentType,
                Content = bytes
            };
            this.itpDbContext.TaskComments.Add(target);
            this.itpDbContext.SaveChanges();
            return target.Id.ToString();
        }

        public async Task<string> AddFileCommentAsync(string taskId, byte commentType, byte[] bytes)
        {
            var target = new TaskComments()
            {
                Id = Guid.NewGuid(),
                TaskId = new Guid(taskId),
                CommentType = commentType,
                Content = bytes
            };
            await this.itpDbContext.TaskComments.AddAsync(target);
            await this.itpDbContext.SaveChangesAsync();
            return target.Id.ToString();
        }

        public int RemoveComment(string? guid)
        {
            var target = itpDbContext.TaskComments.Find(new Guid(guid));
            if (target != null)
            {
                itpDbContext.TaskComments.Remove(target);
                return itpDbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> RemoveCommentAsync(string? guid)
        {
            var target = await itpDbContext.TaskComments.FindAsync(new Guid(guid));
            if (target != null)
            {
                itpDbContext.TaskComments.Remove(target);
                return await itpDbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public TaskComments GetComment(string commentId)
        {
            var target = this.itpDbContext.TaskComments.Find(new Guid(commentId));
            return target;
        }

        public async Task<TaskComments> GetCommentAsync(string commentId)
        {
            var target = await this.itpDbContext.TaskComments.FindAsync(new Guid(commentId));
            return target;
        }

        public int UpdateTask(string taskId, string projectId, string taskName, string startTime, string endTime, string taskDescription)
        {
            var task = itpDbContext.Tasks.Find(new Guid(taskId));
            task.ProjectId = new Guid(projectId);
            task.TaskName = taskName;
            if (String.IsNullOrWhiteSpace(startTime) == false)
            {
                var startHours = int.Parse(startTime.Substring(0, 2));
                var startMinutes = int.Parse(startTime.Substring(3, 2));
                task.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHours, startMinutes, 0);
            }
            if (String.IsNullOrWhiteSpace(endTime) == false)
            {
                var endHours = int.Parse(endTime.Substring(0, 2));
                var endMinutes = int.Parse(endTime.Substring(3, 2));
                task.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, endHours, endMinutes, 0);
            }
            else
            {
                task.EndDate = null;
            }
            task.TaskDescription = taskDescription;
            task.UpdateDate = DateTime.Now;
            itpDbContext.Update(task);
            return itpDbContext.SaveChanges();
        }

        public async Task<int> UpdateTaskAsync(string taskId, string projectId, string taskName, string startTime, string endTime, string taskDescription)
        {
            var task = itpDbContext.Tasks.Find(new Guid(taskId));
            task.ProjectId = new Guid(projectId);
            task.TaskName = taskName;
            if (String.IsNullOrWhiteSpace(startTime) == false)
            {
                var startHours = int.Parse(startTime.Substring(0, 2));
                var startMinutes = int.Parse(startTime.Substring(3, 2));
                task.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHours, startMinutes, 0);
            }
            if (String.IsNullOrWhiteSpace(endTime) == false)
            {
                var endHours = int.Parse(endTime.Substring(0, 2));
                var endMinutes = int.Parse(endTime.Substring(3, 2));
                task.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, endHours, endMinutes, 0);
            }
            else
            {
                task.EndDate = null;
            }
            task.TaskDescription = taskDescription;
            task.UpdateDate = DateTime.Now;
            itpDbContext.Update(task);
            return await itpDbContext.SaveChangesAsync();
        }
    }
}
