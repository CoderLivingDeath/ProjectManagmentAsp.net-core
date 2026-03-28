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

        /// <summary>
        /// Retrieves projects with start date within the specified range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The optional end date of the range. If not provided, returns only projects starting on the exact start date.</param>
        /// <returns>An operation result containing matching projects.</returns>
        public async Task<OperationResult<IEnumerable<Project>>> GetProjectsByStartDateAsync(DateOnly startDate, DateOnly? endDate = null)
        {
            try
            {
                Expression<Func<Project, bool>> filter;
                if (endDate.HasValue)
                {
                    filter = p => p.StartDate >= startDate && p.StartDate <= endDate.Value;
                }
                else
                {
                    filter = p => p.StartDate == startDate;
                }

                var projects = await _projectRepository.FindAsync(filter);
                return OperationResult<IEnumerable<Project>>.Ok(projects);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Project>>.FromException(ex, "Failed to retrieve projects by start date.");
            }
        }

        /// <summary>
        /// Retrieves all projects with the specified priority.
        /// </summary>
        /// <param name="priority">The priority value to filter by.</param>
        /// <returns>An operation result containing matching projects.</returns>
        public async Task<OperationResult<IEnumerable<Project>>> GetProjectsByPriorityAsync(int priority)
        {
            try
            {
                var projects = await _projectRepository.FindAsync(p => p.Priority == priority);
                return OperationResult<IEnumerable<Project>>.Ok(projects);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Project>>.FromException(ex, "Failed to retrieve projects by priority.");
            }
        }

        /// <summary>
        /// Retrieves all projects sorted by name in ascending order.
        /// </summary>
        /// <returns>An operation result containing all projects sorted by name.</returns>
        public async Task<OperationResult<IEnumerable<Project>>> GetProjectsSortedByNameAsync()
        {
            try
            {
                var projects = await _projectRepository.GetAllAsync();
                var sorted = projects.OrderBy(p => p.Name);
                return OperationResult<IEnumerable<Project>>.Ok(sorted);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Project>>.FromException(ex, "Failed to sort projects by name.");
            }
        }

        /// <summary>
        /// Retrieves all projects sorted by start date in ascending order.
        /// </summary>
        /// <returns>An operation result containing all projects sorted by start date.</returns>
        public async Task<OperationResult<IEnumerable<Project>>> GetProjectsSortedByStartDateAsync()
        {
            try
            {
                var projects = await _projectRepository.GetAllAsync();
                var sorted = projects.OrderBy(p => p.StartDate);
                return OperationResult<IEnumerable<Project>>.Ok(sorted);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Project>>.FromException(ex, "Failed to sort projects by start date.");
            }
        }

        /// <summary>
        /// Retrieves all projects sorted by priority in ascending order.
        /// </summary>
        /// <returns>An operation result containing all projects sorted by priority.</returns>
        public async Task<OperationResult<IEnumerable<Project>>> GetProjectsSortedByPriorityAsync()
        {
            try
            {
                var projects = await _projectRepository.GetAllAsync();
                var sorted = projects.OrderBy(p => p.Priority);
                return OperationResult<IEnumerable<Project>>.Ok(sorted);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<Project>>.FromException(ex, "Failed to sort projects by priority.");
            }
        }
    }
}
