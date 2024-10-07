using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Domain;
using CleanArchitecture.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _context.LeaveTypes.AnyAsync(q => q.Name == name);
        }
    }
}
