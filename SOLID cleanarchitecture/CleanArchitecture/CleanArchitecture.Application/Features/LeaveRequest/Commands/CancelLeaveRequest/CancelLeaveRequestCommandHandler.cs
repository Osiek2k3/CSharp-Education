using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Email;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            leaveRequest.Cancelled = true;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // if already approved, re-evaluate the employee's allocations for the leave type
            if(leaveRequest.Approved == true)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                var allocation = await _leaveAllocationRepository.GetUserAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                allocation.NumberOfDays += daysRequested;

                await _leaveAllocationRepository.UpdateAsync(allocation);
            }

            // send confirmation email
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                        $"has been cancelled successfully.",
                    Subject = "Leave Request Cancelled"
                };

                await _emailSender.SendEmail(email);
            }catch (Exception )
            {
                //log
            }

            
            return Unit.Value;
        }
    }
}
