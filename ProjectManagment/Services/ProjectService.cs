using ProjectManagment.Data.Entity;
using ProjectManagment.DTO;
using ProjectManagment.Infrastructure;
using ProjectManagment.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectManagment.Services
{
    public class ProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Company> _companyRepository;

        public ProjectService(IRepository<Project> projectRepository, IRepository<Company> companyRepository)
        {
            _projectRepository = projectRepository;
            _companyRepository = companyRepository;
        }

        public async Task<OperationResult<Guid>> CreateProject(ProjectCreateRequest data)
        {
            try
            {
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

        public async Task<OperationResult<IEnumerable<Project>>> GetProjectsByFilter(Expression<Func<Project, bool>> filter)
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
