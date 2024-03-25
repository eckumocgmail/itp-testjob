using ItpDal;
using ItpDal.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItpBusiness.Services
{
    public class ProjectsService
    {
        private readonly ItpDbContext itpDbContext;

        public ProjectsService( ItpDbContext itpDbContext )
        {
            this.itpDbContext = itpDbContext;
        }

        public IEnumerable<Project> GetProjects()
            => this.itpDbContext.Projects.Include(project => project.Tasks).AsNoTracking().ToList();

        public async Task<IEnumerable<Project>> GetProjectsAsync()
            => await this.itpDbContext.Projects.Include(project => project.Tasks).AsNoTracking().ToListAsync();
    }
}
