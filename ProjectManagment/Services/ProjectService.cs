using ProjectManagment.Data.Entity;
using ProjectManagment.DTO;
using ProjectManagment.Infrastructure;
using ProjectManagment.Interfaces;
using System.Linq.Expressions;

namespace ProjectManagment.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Company> _companyRepository;

        public ProjectService(IRepository<Project> projectRepository, IRepository<Company> companyRepository)
        {
            _projectRepository = projectRepository;
            _companyRepository = companyRepository;
        }

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
