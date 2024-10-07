using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Domain;
using CleanArchitecture.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistance.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(q => !string.IsNullOrEmpty(q.RequestingEmployeeId))
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }

        public Task<LeaveRequest> GetLeaveRequestsWithDetails(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _context.LeaveRequests
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(q => q.LeaveType)
                .ToListAsync();
            return leaveRequests;
        }
    }
}
