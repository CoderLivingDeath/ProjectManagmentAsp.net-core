using ProjectManagment.Data.Entity;
using ProjectManagment.Data.Context;

namespace ProjectManagment.Infrastructure.Repository
{
    public class ProjectRepository : EFRepositoryBase<Project>
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }
    }
}
