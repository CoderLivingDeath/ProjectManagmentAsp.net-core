using ProjectManagment.Data.Entity;
using ProjectManagment.DTO;
using ProjectManagment.Infrastructure;
using ProjectManagment.Interfaces;
using System.Linq.Expressions;

namespace ProjectManagment.Services
{
    /// <summary>
    /// Service for managing project-related operations.
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Company> _companyRepository;

        public ProjectService(IRepository<Project> projectRepository, IRepository<Company> companyRepository)
        {
            _projectRepository = projectRepository;
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// Creates a new project with the specified data.
        /// Validates that start date is not after end date and that both companies exist.
        /// </summary>
        /// <param name="data">The project creation request containing project details.</param>
        /// <returns>An operation result containing the created project ID on success.</returns>
        public async Task<OperationResult<Guid>> CreateProjectAsync(ProjectCreateRequest data)
        {
            try
            {
                if(data.StartDate > data.EndDate) return OperationResult<Guid>.Fail("The end date is greater than the start date.");

                var CustomerEnter = await _companyRepository.GetByIdAsync(data.CustomerCompanyId);

                if (CustomerEnter == null) return OperationResult<Guid>.Fail("Client does not exist.");

                var ExecuterEnter = await _companyRepository.GetByIdAsync(data.ExecuterCompanyId);

                if (ExecuterEnter == null) return OperationResult<Guid>.Fail("Executer does not exist.");

                var newEnter = await _projectRepository.AddAsync(data);

                if (newEnter == null) return OperationResult<Guid>.Fail("Project was not created");

                return OperationResult<Guid>.Ok(newEnter.Id);
            }
            catch (Exception ex)
            {
                return OperationResult<Guid>.FromException(ex, "Fail on creating project.");
            }
        }

        /// <summary>
        /// Retrieves all projects that match the specified filter.
        /// </summary>
        /// <param name="filter">A function to test each project for a condition.</param>
        /// <returns>An operation result containing matching projects.</returns>
        public async Task<OperationResult<IEnumerable<Project>>> GetProjectsByFilterAsync(Expression<Func<Project, bool>> filter)
        {
            try
            {
                var projects = await _projectRepository.FindAsync(filter);

                return OperationResult<IEnumerable<Project>>.Ok(projects);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Project>>.FromException(ex, "Failed to retrieve projects.");
            }
        }
    }
}
