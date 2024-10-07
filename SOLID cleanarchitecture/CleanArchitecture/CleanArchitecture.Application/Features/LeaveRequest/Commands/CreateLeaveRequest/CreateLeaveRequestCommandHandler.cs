using AutoMapper;
using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Models.Email;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUserServices _userServices;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CreateLeaveRequestCommandHandler(IEmailSender emailSender,
            IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
            ILeaveRequestRepository leaveRequestRepository, IUserServices userServices,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            _emailSender = emailSender;
            _mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveRequestRepository = leaveRequestRepository;
            _userServices = userServices;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);

            // Get requesting employee's id
            var employeeId = _userServices.UserId;
            // Check on employee's allocation
            var allocation = await _leaveAllocationRepository.GetUserAllocation(employeeId,request.LeaveTypeId);
            // if allocations aren't enough, return validation error with message
            if (allocation == null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId),
                    "You do not have any allocations for this leave type"));
                throw new  BadRequestException("Invalid Leave Request",validationResult);
            }

            int daysRequest = (int)(request.EndDate - request.StartDate).TotalDays;
            if(daysRequest > allocation.NumberOfDays)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.EndDate),
                    "You do not have enough days for this request"));
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            // Create leave request
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            leaveRequest.RequestingEmployeeId = employeeId;
            leaveRequest.DataRequested = DateTime.Now;
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            // send confirmation email
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                        $"has been submitted successfully.",
                    Subject = "Leave Request Submitted"
                };

                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                //log
            }
            

            return Unit.Value;
        }
    }
}
