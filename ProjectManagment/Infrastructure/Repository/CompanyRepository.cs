using ProjectManagment.Data.Entity;
using ProjectManagment.Data.Context;

namespace ProjectManagment.Infrastructure.Repository
{
    public class CompanyRepository : EFRepositoryBase<Company>
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }
    }
}
