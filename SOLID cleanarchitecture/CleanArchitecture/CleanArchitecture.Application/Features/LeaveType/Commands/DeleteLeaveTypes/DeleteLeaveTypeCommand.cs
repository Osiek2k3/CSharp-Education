using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Commands.DeleteLeaveTypes
{
    public class DeleteLeaveTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }

    }
}
