using Microsoft.AspNetCore.Http;
using ProjectManagment.Data.Entity;

namespace ProjectManagment.DTO
{
    public class ProjectCreateRequest
    {
        public required string Name { get; set; }
        public required DateOnly StartDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required int Priority { get; set; }
        public required Guid CustomerCompanyId { get; set; }
        public required Guid ExecuterCompanyId { get; set; }
        public Guid? ManagerId { get; set; }
        public List<Guid> EmployeeIds { get; set; } = new();
        public List<IFormFile> Documents { get; set; } = new();

        public static implicit operator Project(ProjectCreateRequest request)
        {
            return new Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Priority = request.Priority,
                CompanyCustomerId = request.CustomerCompanyId,
                CompanyExecuterId = request.ExecuterCompanyId
            };
        }
    }
}
