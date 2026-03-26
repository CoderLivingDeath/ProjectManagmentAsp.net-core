using ProjectManagment.Data.Entity;
using ProjectManagment.Data.Context;

namespace ProjectManagment.Infrastructure.Repository
{
    public class EmployeeRepository : EFRepositoryBase<Employee>
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
