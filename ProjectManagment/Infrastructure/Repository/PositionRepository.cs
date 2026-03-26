using ProjectManagment.Data.Entity;
using ProjectManagment.Data.Context;

namespace ProjectManagment.Infrastructure.Repository
{
    public class PositionRepository : EFRepositoryBase<Position>
    {
        public PositionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
