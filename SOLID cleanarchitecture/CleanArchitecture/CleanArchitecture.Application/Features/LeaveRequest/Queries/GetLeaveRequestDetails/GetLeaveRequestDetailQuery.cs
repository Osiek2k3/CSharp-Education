using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDetailsDto>
    {
        public int Id { get; set; }
    }
}
