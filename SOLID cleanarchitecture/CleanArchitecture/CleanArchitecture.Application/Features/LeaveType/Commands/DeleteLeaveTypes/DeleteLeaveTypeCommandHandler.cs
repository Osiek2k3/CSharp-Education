using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveType.Commands.DeleteLeaveTypes
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (leaveTypeToDelete == null)
            {
                throw new NotFoundException(nameof(LeaveType),request.Id);
            }

            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            return Unit.Value;
        }
    }
}
