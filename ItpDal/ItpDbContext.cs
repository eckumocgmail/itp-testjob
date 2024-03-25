using ItpDal.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItpDal
{
    public class ItpDbContext : DbContext
    {
        public DbSet<ItpDal.Entities.Project> Projects { get; set; }
        public DbSet<ItpDal.Entities.Task> Tasks { get; set; }
        public DbSet<ItpDal.Entities.TaskComments> TaskComments { get; set; }

        public ItpDbContext()
        {
        }

        public ItpDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Itp.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItpDal.Entities.Project>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ItpDal.Entities.Task>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ItpDal.Entities.TaskComments>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");

            this.OnModelCreatingData(modelBuilder);
        }

        private void OnModelCreatingData(ModelBuilder modelBuilder)
        {
            var projectId1 = Guid.NewGuid();
            var projectId2 = Guid.NewGuid();
            var projectId3 = Guid.NewGuid();
            modelBuilder.Entity<ItpDal.Entities.Project>().HasData(
                new Project()
                {
                    Id = projectId1,
                    ProjectName = "ProjectName1",
                    CreateDate = DateTime.Parse("01.03.2024"),
                    UpdateDate = DateTime.Parse("01.03.2024")
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Project>().HasData(
                new Project()
                {
                    Id = projectId2,
                    ProjectName = "ProjectName2",
                    CreateDate = DateTime.Parse("02.03.2024"),
                    UpdateDate = DateTime.Parse("02.03.2024")
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Project>().HasData(
                new Project()
                {
                    Id = projectId3,
                    ProjectName = "ProjectName3",
                    CreateDate = DateTime.Parse("01.01.2024"),
                    UpdateDate = DateTime.Parse("01.01.2024")
                }
            );
            var taskId11 = Guid.NewGuid();
            var taskId12 = Guid.NewGuid();
            var taskId13 = Guid.NewGuid();
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId11,
                    TaskName = "TaskName1",
                    ProjectId = projectId1,
                    CancelDate = null,
                    StartDate = DateTime.Parse("01.03.2024"),                    
                    EndDate = new DateTime(2024, 3, 1, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("01.03.2024"),
                    UpdateDate = DateTime.Parse("01.03.2024")
                }
            );
            modelBuilder.Entity<ItpDal.Entities.TaskComments>().HasData(
                new ItpDal.Entities.TaskComments()
                {
                    TaskId = taskId11,
                    CommentType = 1,
                    Id = Guid.NewGuid(),
                    Content = "This is a test 1".ToArray().Select(ch => (byte)ch).ToArray()
                }
            );
            modelBuilder.Entity<ItpDal.Entities.TaskComments>().HasData(
                new ItpDal.Entities.TaskComments()
                {
                    TaskId = taskId12,
                    CommentType = 1,
                    Id = Guid.NewGuid(),
                    Content = "This is a test 2".ToArray().Select(ch => (byte)ch).ToArray()
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId12,
                    TaskName = "TaskName2",
                    ProjectId = projectId1,
                    CancelDate = null,
                    StartDate = DateTime.Parse("02.03.2024"),
                    EndDate = new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("02.03.2024"),
                    UpdateDate = DateTime.Parse("02.03.2024")
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId13,
                    TaskName = "TaskName3",
                    ProjectId = projectId1,
                    CancelDate = null,
                    StartDate = DateTime.Parse("02.03.2024"),
                    EndDate = new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("02.03.2024"),
                    UpdateDate = DateTime.Parse("02.03.2024")
                }
            );
            var taskId21 = Guid.NewGuid();
            var taskId22 = Guid.NewGuid();
            var taskId23 = Guid.NewGuid();
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId21,
                    TaskName = "TaskName1",
                    ProjectId = projectId2,
                    CancelDate = null,
                    StartDate = DateTime.Parse("03.03.2024"),
                    EndDate = new DateTime(2024, 3, 3, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("03.03.2024"),
                    UpdateDate = DateTime.Parse("03.03.2024")
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId22,
                    TaskName = "TaskName2",
                    ProjectId = projectId2,
                    CancelDate = null,
                    StartDate = DateTime.Parse("02.03.2024"),
                    EndDate = new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("02.03.2024"),
                    UpdateDate = DateTime.Parse("02.03.2024")
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId23,
                    TaskName = "TaskName3",
                    ProjectId = projectId2,
                    CancelDate = null,
                    StartDate = DateTime.Parse("02.03.2024"),
                    EndDate = new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("02.03.2024"),
                    UpdateDate = DateTime.Parse("02.03.2024")
                }
            );
            var taskId31 = Guid.NewGuid();
            var taskId32 = Guid.NewGuid();
            var taskId33 = Guid.NewGuid();
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId31,
                    TaskName = "TaskName1",
                    ProjectId = projectId3,
                    CancelDate = null,
                    StartDate = DateTime.Parse("01.03.2024"),
                    CreateDate = DateTime.Parse("01.03.2024"),
                    UpdateDate = DateTime.Parse("01.03.2024"),
                    EndDate = new DateTime(2024, 3, 1, 0, 1, 1, 1, DateTimeKind.Unspecified),
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId32,
                    TaskName = "TaskName2",
                    ProjectId = projectId3,
                    CancelDate = null,
                    StartDate = DateTime.Parse("02.03.2024"),
                    EndDate = new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("02.03.2024"),
                    UpdateDate = DateTime.Parse("02.03.2024")
                }
            );
            modelBuilder.Entity<ItpDal.Entities.Task>().HasData(
                new ItpDal.Entities.Task()
                {
                    Id = taskId33,
                    TaskName = "TaskName3",
                    ProjectId = projectId3,
                    CancelDate = null,
                    StartDate = DateTime.Parse("02.03.2024"),
                    EndDate = new DateTime(2024, 3, 2, 0, 1, 1, 1, DateTimeKind.Unspecified),
                    CreateDate = DateTime.Parse("02.03.2024"),
                    UpdateDate = DateTime.Parse("02.03.2024")
                }
            );
        }
    }
}
