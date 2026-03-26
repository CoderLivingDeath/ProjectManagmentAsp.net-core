using ProjectManagment.Data.Entity;
using ProjectManagment.DTO;
using ProjectManagment.Infrastructure;
using System.Linq.Expressions;

namespace ProjectManagment.Interfaces
{
    public interface IProjectService
    {
        Task<OperationResult<Guid>> CreateProjectAsync(ProjectCreateRequest data);
        Task<OperationResult<IEnumerable<Project>>> GetProjectsByFilterAsync(Expression<Func<Project, bool>> filter);
    }
}