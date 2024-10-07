using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public record GetLeaveTypesDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
}
