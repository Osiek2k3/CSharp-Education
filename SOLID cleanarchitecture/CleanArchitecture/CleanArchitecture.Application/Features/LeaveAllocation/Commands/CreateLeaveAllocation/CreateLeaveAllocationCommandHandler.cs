using AutoMapper;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Contracts.Persistance;
using CleanArchitecture.Application.Exceptions;
using MediatR;

namespace CleanArchitecture.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUserServices _userServices;

        public CreateLeaveAllocationCommandHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository,
            IUserServices userServices)
        {
            _mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            _userServices = userServices;
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

            // Get Leave type for allocations
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            // Get Employees
            var empolyees = await _userServices.GetEmployees();

            //Get Period
            var period = DateTime.Now.Year;

            //Assign Allocations
            var allocations = new List<Domain.LeaveAllocation>();
            foreach ( var employee in empolyees )
            {
                var allocationExists = await _leaveAllocationRepository.AllocationExists(employee.Id, request.LeaveTypeId, period);

                if( allocationExists == false)
                {
                    allocations.Add(new Domain.LeaveAllocation { 
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period,
                    });
                }
            }

            if(allocations.Any())
            {
                await _leaveAllocationRepository.AddAllocations(allocations);
            }

            return Unit.Value;
        }
    }
}
