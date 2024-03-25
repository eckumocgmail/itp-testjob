using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItpDal.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; } = "";
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; } = null;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? CancelDate { get; set; } = null;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public IEnumerable<TaskComments> TaskComments { get; set;}
    }
}
