using uiii.Services.Base;

namespace uiii.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId);
    }
}
